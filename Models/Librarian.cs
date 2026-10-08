using System;
using System.Collections.Generic;
using System.Text;

namespace CityLibraryManagementSystem.Models
{
    public class Librarian : Person
    {
        private string _employeeCode;

        public string EmployeeCode
        {
            get
            {
                return _employeeCode;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exceptions.InvalidInputException(
                        "Employee code cannot be empty."
                    );
                }

                _employeeCode = value;
            }
        }

        public Librarian(int id, string name, string employeeCode)
            : base(id, name)
        {
            EmployeeCode = employeeCode;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine(
                $"Librarian ID: {Id} | Name: {Name} | Employee Code: {EmployeeCode}"
            );
        }
    }
}
