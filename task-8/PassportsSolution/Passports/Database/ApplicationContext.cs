using Microsoft.EntityFrameworkCore;
using Passports.Models;
using Passports.Models.Configuration;

namespace Passports.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Passport> InactivePassports { get; set; }
        public DbSet<UssrPassport> InactiveUssrPassports { get; set; }
        public DbSet<PassportHistory> PassportHistory { get; set; }
        public DbSet<UssrPassportHistory> UssrPassportHistory { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InactivePassportsConfiguration());
            modelBuilder.ApplyConfiguration(new InactiveUssrPassportsConfiguration());
            modelBuilder.ApplyConfiguration(new PassportHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new  UssrPassportHistoryConfiguration());
        }
    }
}
