using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sourceful.Domain.Entities
{
    public class UserAddress
    {
        public Guid UserAddressId { get; set; }

        public string StreetName { get; set; }

        public int Number { get; set; }

        public int PostCode { get; set; }
    }
}
