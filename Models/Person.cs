using System;
using System.Collections.Generic;
using System.Text;

namespace CityLibraryManagementSystem.Models
{
    public abstract class Person
    {
        public int Id { get; protected set; }

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exceptions.InvalidInputException(
                        "Name cannot be empty."
                    );
                }

                _name = value;
            }
        }

        protected Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public abstract void DisplayInfo();
    }
}
