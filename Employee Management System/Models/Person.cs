using Employee_Management_System.Exceptions;
using EmployeeManagementSystem.Exceptions;

namespace EmployeeManagementSystem.Models
{
    public abstract class Person
    {
        private string _name;

        public int Id { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new NameEmptyException("Name boş ola bilməz.");
                if (value.Length < 3 || value.Length > 25)
                    throw new NameLengthException("Name 3-25 simvol arasında olmalıdır.");
                _name = value;
            }
        }

        public abstract void PrintInfo();
    }
}