
using EmployeeManagementSystem.Exceptions;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.Services
{
    public static class EmployeeService
    {
        public static List<Employee> employees = new List<Employee>();
        public static string path = "C:\\Users\\ca r221.14\\source\\repos\\EmployeeManagementSystem\\Data\\employees.json.cs";

        public static void SaveToFile()
        {
            var json = JsonConvert.SerializeObject(employees, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public static void LoadFromFile()
        {
            try
            {
                if (!File.Exists(path))
                {
                    employees = new List<Employee>();
                    return;
                }
                string json = File.ReadAllText(path);
                employees = JsonConvert.DeserializeObject<List<Employee>>(json) ?? new List<Employee>();
            }
            catch (Exception ex)
            {
                throw new System.IO.FileLoadException("File oxunarken xeta bas verdi.");
            }
        }

        public static void AddEmployee(Employee emp)
        {
            if (employees.Any(e => e.Id == emp.Id))
                throw new DuplicateEmployeeException("Bu isci artiq sistemde movcuddur.");
            employees.Add(emp);
            SaveToFile();
            Console.WriteLine("İsci ugurla elave olundu.");
        }

        public static void RemoveEmployee(int id)
        {
            var emp = employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                throw new EmployeeNotFoundException("Sistemde bele isci yoxdur.");
            employees.Remove(emp);
            SaveToFile();
            Console.WriteLine("İşçi uğurla silindi.");
        }

        public static Employee SearchEmployee(int id)
        {
            var emp = employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                throw new EmployeeNotFoundException("Sistemde bele isci yoxdur.");
            return emp;
        }
    }

        public static void UpdateEmployee(Employee updated)
        {
            var emp = employees.FirstOrDefault(e => e.Id == updated.Id);
            if (emp == null)
                throw new EmployeeNotFoundException("Sistemde bele isci yoxdur.");

            emp.Name = updated.Name;
            emp.Age = updated.Age;
            emp.Position = updated.Position;
            emp.Department = updated.Department;
            emp.Salary = updated.Salary;
            emp.WorkInfo = updated.WorkInfo;

            SaveToFile();
            Console.WriteLine("İsci melumatlari yenilendi.");
        }

        public static List<Employee> AllEmployees() => employees.ToList();

        public static List<Employee> SortEmployees(string choice)
        {
            switch (choice.Trim().ToLower())
            {
                case "id": return employees.OrderBy(e => e.Id).ToList();
                case "name": return employees.OrderBy(e => e.Name).ToList();
                case "hiredate": return employees.OrderBy(e => e.HireDate).ToList();
                case "salary": return employees.OrderBy(e => e.Salary ?? 0).ToList();
                default: return employees;
            }
        

        public static List<Employee> FilterByName(string part) =>
            employees.Where(e => e.Name.Contains(part, StringComparison.OrdinalIgnoreCase)).ToList();

        public static List<Employee> FilterBySalary(decimal min, decimal max) =>
            employees.Where(e => e.Salary.HasValue && e.Salary.Value >= min && e.Salary.Value <= max).ToList();
        }
}

       


