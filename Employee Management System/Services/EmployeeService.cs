
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
        }

        public static List<Employee> FilterByName(string part) =>
            employees.Where(e => e.Name.Contains(part, StringComparison.OrdinalIgnoreCase)).ToList();

        public static List<Employee> FilterBySalary(decimal min, decimal max) =>
            employees.Where(e => e.Salary.HasValue && e.Salary.Value >= min && e.Salary.Value <= max).ToList();


        static void AddEmployeeFlow()
        {
            Console.Write("ID: "); int id = int.Parse(Console.ReadLine());
            Console.Write("Name: "); string name = Console.ReadLine();
            Console.Write("Age: "); string? ageStr = Console.ReadLine();
            int? age = string.IsNullOrEmpty(ageStr) ? null : int.Parse(ageStr);
            Console.Write("Position (Junior/Middle/Senior/Manager): "); string posStr = Console.ReadLine();
            Position? pos = string.IsNullOrEmpty(posStr) ? null : Enum.Parse<Position>(posStr);
            Console.Write("Department: "); string dept = Console.ReadLine();
            Console.Write("Salary: "); string salStr = Console.ReadLine();
            decimal? salary = string.IsNullOrEmpty(salStr) ? null : decimal.Parse(salStr);
            Console.Write("Office Number: "); string office = Console.ReadLine();
            Console.Write("Floor: "); int floor = int.Parse(Console.ReadLine());
            Contact contact = new Contact(office, floor);
            DateTime hireDate = DateTime.Now;

            Employee emp = new Employee(id, name, contact, dept, hireDate, age, pos, salary);
            EmployeeService.AddEmployee(emp);

            EmployeeService.SaveToFile();
        }

        static void RemoveEmployeeFlow()
        {
            Console.Write("Silinəcək Employee ID: "); int id = int.Parse(Console.ReadLine());
            EmployeeService.RemoveEmployee(id);
        }

        static void SearchEmployeeFlow()
        {
            Console.Write("Axtarılacaq Employee ID: "); int id = int.Parse(Console.ReadLine());
            Employee emp = EmployeeService.SearchEmployee(id);
            emp?.PrintInfo();
        }

        static void UpdateEmployeeFlow()
        {
            Console.Write("ID: "); int id = int.Parse(Console.ReadLine());
            Console.Write("Name: "); string name = Console.ReadLine();
            Console.Write("Age: "); string? ageStr = Console.ReadLine();
            int? age = string.IsNullOrEmpty(ageStr) ? null : int.Parse(ageStr);
            Console.Write("Position: "); string posStr = Console.ReadLine();
            Position? pos = string.IsNullOrEmpty(posStr) ? null : Enum.Parse<Position>(posStr);
            Console.Write("Department: "); string dept = Console.ReadLine();
            Console.Write("Salary: "); string salStr = Console.ReadLine();
            decimal? salary = string.IsNullOrEmpty(salStr) ? null : decimal.Parse(salStr);
            Console.Write("Office Number: "); string office = Console.ReadLine();
            Console.Write("Floor: "); int floor = int.Parse(Console.ReadLine());
            Contact contact = new Contact(office, floor);

            Employee updated = new Employee(id, name, contact, dept, DateTime.Now, age, pos, salary);
            EmployeeService.UpdateEmployee(updated);
        }

        static void ListEmployeesFlow()
        {
            var list = EmployeeService.AllEmployees();
            list.ForEach(e => e.PrintInfo());
        }

        static void SortEmployeesFlow()
        {
            Console.Write("Sort by (id/name/hiredate/salary): "); string choice = Console.ReadLine();
            var sorted = EmployeeService.SortEmployees(choice);
            sorted.ForEach(e => e.PrintInfo());
        }

        static void FilterEmployeesFlow()
        {
            Console.WriteLine("Filter by: 1) Name  2) Salary range");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Write("Name part: ");
                string part = Console.ReadLine();
                var filtered = EmployeeService.FilterByName(part);
                filtered.ForEach(e => e.PrintInfo());
            }
            else if (choice == "2")
            {
                Console.Write("Min salary: "); decimal min = decimal.Parse(Console.ReadLine());
                Console.Write("Max salary: "); decimal max = decimal.Parse(Console.ReadLine());
                var filtered = EmployeeService.FilterBySalary(min, max);
                filtered.ForEach(e => e.PrintInfo());
            }
        }



    }
}


