using Microsoft.EntityFrameworkCore;
using Sourceful.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sourceful.Infrastructure
{
    public class DbSourcefulTask : DbContext
    {
        public DbSourcefulTask(DbContextOptions<DbSourcefulTask> options) : base(options)
        { 
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserAddress> UserAddresses { get; set; }

        public DbSet<UserSetting> UserSettings { get; set; }
    }
}
