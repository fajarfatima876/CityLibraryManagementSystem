using System;
using System.Collections.Generic;
using System.Text;

namespace CityLibraryManagementSystem.Exceptions
{
    internal class BookNotFoundException : LibraryException

    {
        public BookNotFoundException(string message) : base(message)
        {

        }
    }

}
