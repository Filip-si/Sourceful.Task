using System;

namespace Sourceful.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Guid UserAddressId { get; set; }

        public UserAddress Address { get; set; }

        public Guid UserSettingId { get; set; }

        public UserSetting Setting { get; set; }

    }
}
