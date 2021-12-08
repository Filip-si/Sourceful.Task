using System;

namespace Sourceful.Application.Models
{
    public class UserResponse
    {
        public UserResponse(Guid userId, string firstName, string lastName, int age, string streetName, int number, int postCode, string email, string name)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            StreetName = streetName;
            Number = number;
            PostCode = postCode;
            Email = email;
            Name = name;
        }

        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string StreetName { get; set; }

        public int Number { get; set; }

        public int PostCode { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}
