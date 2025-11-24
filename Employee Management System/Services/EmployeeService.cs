using EmployeeManagementSystem.Exceptions;
using EmployeeManagementSystem.Models;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.Services
{
    public static class EmployeeService
    {
        public static List<Employee> employees = new List<Employee>();

        public static string path = "C:\\Users\\PC\\source\\repos\\Employee Management System\\Employee Management System\\Data\\employees.json";

        public static void SaveToFile()
        {
            string json = JsonConvert.SerializeObject(employees, Formatting.Indented);
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
                employees = JsonConvert.DeserializeObject<List<Employee>>(json)
                            ?? new List<Employee>();
            }
            catch
            {
                throw new System.IO.FileLoadException("Fayl oxunarken xəta baş verdi.");
            }
        }

        public static void AddEmployee(Employee emp)
        {
            if (employees.Any(e => e.Id == emp.Id))
                throw new DuplicateEmployeeException("Bu işçi artıq mövcuddur.");

            employees.Add(emp);
            SaveToFile();
            Console.WriteLine("İşçi əlavə olundu.");
        }

        public static void RemoveEmployee(int id)
        {
            var emp = employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                throw new EmployeeNotFoundException("İşçi tapılmadı.");

            employees.Remove(emp);
            SaveToFile();
            Console.WriteLine("İşçi silindi.");
        }

        public static Employee SearchEmployee(int id)
        {
            var emp = employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                throw new EmployeeNotFoundException("İşçi tapılmadı.");
            return emp;
        }

        public static void UpdateEmployee(Employee updated)
        {
            var emp = employees.FirstOrDefault(e => e.Id == updated.Id);
            if (emp == null)
                throw new EmployeeNotFoundException("İşçi tapılmadı.");

            emp.Name = updated.Name;
            emp.Age = updated.Age;
            emp.Position = updated.Position;
            emp.Department = updated.Department;
            emp.Salary = updated.Salary;
            emp.WorkInfo = updated.WorkInfo;

            SaveToFile();
            Console.WriteLine("Məlumat yeniləndi.");
        }

        public static List<Employee> AllEmployees() =>
            employees.ToList();

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
        }

        public static List<Employee> FilterByName(string part)
        {
            return employees
                .Where(e => e.Name.Contains(part, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public static List<Employee> FilterBySalary(decimal min, decimal max)
        {
            return employees
                .Where(e => e.Salary >= min && e.Salary <= max)
                .ToList();
        }
    }
}

