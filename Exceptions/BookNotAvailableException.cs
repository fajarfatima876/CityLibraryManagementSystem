using System;
using System.Collections.Generic;
using System.Text;

namespace CityLibraryManagementSystem.Exceptions
{
    public class BookNotAvailableException : LibraryException
    {
        public BookNotAvailableException(string message) : base(message)
        {

        }
    }
}
