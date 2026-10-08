using System;
using System.Collections.Generic;
using System.Text;

namespace CityLibraryManagementSystem.Exceptions
{
    public abstract class
         LibraryException :
         Exception
    {
        protected LibraryException(string message) : base(message)
        {

        }
    }
}
