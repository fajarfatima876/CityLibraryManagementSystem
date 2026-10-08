using System;

namespace CityLibraryManagementSystem.Models
{
    public class Member : Person
    {
        private string _phone;

        private static int _nextMemberId = 1;

        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exceptions.InvalidInputException(
                        "Phone cannot be empty."
                    );
                }

                _phone = value;
            }
        }

        // New member ke liye
        public Member(string name, string phone)
            : base(_nextMemberId++, name)
        {
            Phone = phone;
        }

        // Saved member ko load karne ke liye
        public Member(int id, string name, string phone)
            : base(id, name)
        {
            Phone = phone;

            if (id >= _nextMemberId)
            {
                _nextMemberId = id + 1;
            }
        }

        public override void DisplayInfo()
        {
            Console.WriteLine(
                $"Member ID: {Id} | Name: {Name} | Phone: {Phone}"
            );
        }
    }
}
