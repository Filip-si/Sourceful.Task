using Microsoft.EntityFrameworkCore;
using Sourceful.Domain.Entities;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(ua => ua.User)
                .HasForeignKey<UserAddress>(u => u.UserId);

            builder.Entity<User>()
                .HasOne(u => u.Setting)
                .WithOne(us => us.User)
                .HasForeignKey<UserSetting>(u => u.UserId);
        }
    }
}
