using System;

namespace Sourceful.Domain.Entities
{
    public class UserAddress
    {
        public Guid UserAddressId { get; set; }

        public string StreetName { get; set; }

        public int Number { get; set; }

        public int PostCode { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
