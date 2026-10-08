using System;
using System.Collections.Generic;
using System.Text;

namespace CityLibraryManagementSystem.Exceptions
{
    internal class MemberNotFoundException : LibraryException
    
    {
        public MemberNotFoundException(string message) : base(message)
        {

        }
    }

}

