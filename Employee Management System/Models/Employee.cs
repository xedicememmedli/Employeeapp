
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Exceptions;


namespace EmployeeManagementSystem.Models
    {
        public class Employee : Person, IPrintable
        {
            private string _department;
            private decimal? _salary;

            public int? Age { get; set; }
            public Position? Position { get; set; }

            public string Department
            {
                get => _department;
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new NameEmptyException("Department bos ola bilmez.");
                    }
                    if (value.Length < 5 || value.Length > 6)
                    {
                        throw new NameLengthException("Department 5-6 simvol arasinda olmalidir.");
                    }
                    _department = value;
                }
            }

            public decimal? Salary
            {
                get => _salary;
                set
                {
                    if (value.HasValue && value <= 0)
                        throw new InvalidSalaryException("Salary 0-dan boyuk olmalidir.");
                    _salary = value;
                }
            }

            public Contact WorkInfo { get; set; }
            public DateTime HireDate { get; }

            public Employee(int id, string name, Contact workInfo, string department, DateTime hireDate, int? age = null, Position? position = null, decimal? salary = null)
            {
                Id = id;
                Name = name;
                WorkInfo = workInfo;
                Department = department;
                HireDate = hireDate;
                Age = age;
                Position = position;
                Salary = salary;
            }

            public override void PrintInfo()
            {
                PrintEmployeeInfo();
                //    Console.WriteLine($"{Id} | {Name} | {(Age.HasValue ? Age.Value.ToString() : "N/A")} | {(Position.HasValue ? Position.Value.ToString() : "N/A")} | {Department} | {(Salary.HasValue ? Salary.Value.ToString("F2") : "N/A")} | {HireDate:yyyy-MM} | {WorkInfo.OfficeNumber} | Floor: {WorkInfo.Floor}");
            }

            public void PrintEmployeeInfo()
            {
                Console.WriteLine($"{Id} | {Name} | {(Age.HasValue ? Age.Value.ToString() : "N/A")} | {(Position.HasValue ? Position.Value.ToString() : "N/A")} | {Department} | {(Salary.HasValue ? Salary.Value.ToString("F2") : "N/A")} | {HireDate:yyyy-MM} | {WorkInfo.OfficeNumber} | Floor: {WorkInfo.Floor}");
            }
        }
    }
