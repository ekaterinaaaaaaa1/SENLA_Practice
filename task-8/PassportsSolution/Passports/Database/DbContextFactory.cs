using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Passports.Database
{
    /// <summary>
    /// DbContextFactory for migrations.
    /// </summary>
    public class DbContextFactory() : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
            configurationBuilder.AddJsonFile("appsettings.json");
            IConfigurationRoot configurationRoot = configurationBuilder.Build();
            
            string? connectionString = configurationRoot.GetConnectionString("DefaultConnection");
            if (connectionString != null)
            {
                optionsBuilder.UseNpgsql(connectionString);
            }
            
            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
