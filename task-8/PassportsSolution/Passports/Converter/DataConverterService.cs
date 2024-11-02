using Passports.Database;
using Passports.Models;
using Passports.Services;
using System.Diagnostics;

namespace Passports.Converter
{
    public class DataConverterService(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration) : BackgroundService
    {
        private string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Converter", "data1", "Data1.csv");

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            
            await context.Database.EnsureCreatedAsync(stoppingToken);

            using StreamReader streamReader = new StreamReader(_path);
            Debug.WriteLine("ExecuteAsync is working");
            Console.WriteLine("ExecuteAsync is working");

            string? line = streamReader.ReadLine();
            while ((line = streamReader.ReadLine()) != null)
            {
                Passport? passport = CsvParserService.Parse(line);

                if (passport != null)
                {
                    Passport? existingPassport = context.InactivePassports.Find(passport.Series, passport.Number);
                    if (existingPassport != null)
                    {
                        existingPassport.IsActive = false;
                        context.InactivePassports.Update(existingPassport);
                    }
                    else
                    {
                        context.InactivePassports.Add(passport);
                    }
                    context.SaveChanges();
                }
            }

            //context.SaveChanges();

            /*while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(200, stoppingToken);
            }*/

            await Task.CompletedTask;
        }
    }
}
