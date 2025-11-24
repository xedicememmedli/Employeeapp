using System;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Exceptions;

namespace EmployeeManagementSystem
{
    class Program
    {
        static void Main()
        {
            string path = "C:\\Users\\PC\\source\\repos\\Employee Management System\\Employee Management System\\Data\\employees.json";
            EmployeeService.LoadFromFile();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("-----Employee Management System-----");
                Console.WriteLine("1 - Add Employee");
                Console.WriteLine("2 - Remove Employee");
                Console.WriteLine("3 - Search Employee");
                Console.WriteLine("4 - Update Employee");
                Console.WriteLine("5 - List Employees");
                Console.WriteLine("6 - Sort Employees");
                Console.WriteLine("7 - Filter Employees");
                Console.WriteLine("0 - Exit");
                Console.Write("Secim: ");
                int choice = int.Parse(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1: EmployeeService.AddEmployeeFlow(); break;
                        case 2: RemoveEmployeeFlow(); break;
                        case 3: SearchEmployeeFlow(); break;
                        case 4: UpdateEmployeeFlow(); break;
                        case 5: ListEmployeesFlow(); break;
                        case 6: SortEmployeesFlow(); break;
                        case 7: FilterEmployeesFlow(); break;
                        case 0: exit = true; break;
                        default: Console.WriteLine("Yanlis secim"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Xeta: {ex.Message}");
                }
            }
        }

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
    }
}
        

        
