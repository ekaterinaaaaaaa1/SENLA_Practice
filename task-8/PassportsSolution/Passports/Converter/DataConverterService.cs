using Passports.Database;
using Passports.Models;
using Passports.Services;
using System.Diagnostics;

namespace Passports.Converter
{
    public class DataConverterService(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PostgresDBService>();

            // await context.Database.EnsureCreatedAsync(stoppingToken);

            context.Copy();

            await Task.CompletedTask;
        }
    }
}
