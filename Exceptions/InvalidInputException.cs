using System;
using System.Collections.Generic;
using System.Text;

namespace CityLibraryManagementSystem.Exceptions
{
    internal class InvalidInputException : LibraryException

    {
        public InvalidInputException(string message) : base(message)
        {

        }
    }

}
