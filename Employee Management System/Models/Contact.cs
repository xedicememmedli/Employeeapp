
using EmployeeManagementSystem.Exceptions;

namespace EmployeeManagementSystem.Models
{
    public struct Contact
    {
        public string OfficeNumber { get; set; }
        public int Floor { get; set; }

        public Contact(string officeNumber, int floor)
        {
            if (string.IsNullOrWhiteSpace(officeNumber))
                throw new InvalidWorkInfoException("Office number bos ola bilmez.");
            if (floor <= 0)
                throw new InvalidWorkInfoException("Floor 0-dan boyuk olmalidir.");

            OfficeNumber = officeNumber;
            Floor = floor;
        }
    }
}