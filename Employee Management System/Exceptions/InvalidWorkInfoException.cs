using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Exceptions
{
    public class InvalidWorkInfoException : Exception
    {
        public InvalidWorkInfoException(string message) : base(message) { }
    }
}
