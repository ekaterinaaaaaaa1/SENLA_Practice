using Passports.Database;
using Passports.Models;
using Passports.Services;
using Passports.Services.Interfaces;
using System.Diagnostics;

namespace Passports.Converter
{
    public class DataConverterService(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IDBService>();

            // await context.Database.EnsureCreatedAsync(stoppingToken);

            await context.Copy();

            await Task.CompletedTask;
        }
    }
}
