using Microsoft.EntityFrameworkCore;
using Passports.Models;
using Passports.Models.Configuration;

namespace Passports.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Passport> InactivePassports { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InactivePassportsConfiguration());
        }
    }
}
