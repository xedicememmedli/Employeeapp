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
    }
}
        

        