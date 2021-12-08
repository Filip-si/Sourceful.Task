using System;

namespace Sourceful.Application.Models
{
    public class UserUpdateRequest
    {
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
