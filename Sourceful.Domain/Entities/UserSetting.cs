using System;

namespace Sourceful.Domain.Entities
{
    public class UserSetting
    {
        public Guid UserSettingId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
