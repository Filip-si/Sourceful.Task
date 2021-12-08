using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sourceful.Domain.Entities
{
    public class UserSetting
    {
        public Guid UserSettingId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}
