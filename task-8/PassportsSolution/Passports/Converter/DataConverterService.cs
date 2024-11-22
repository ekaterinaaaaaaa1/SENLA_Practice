using Passports.Services.Interfaces;
using Passports.Exceptions;

namespace Passports.Converter
{
    public class DataConverterService(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration) : BackgroundService
    {
        private const int GMT_OFFSET = 3;
        private readonly string _sectionName = "ReadingCsvTime";

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IDBService>();
            double readingCsvTotalMinutes = -1;

            try
            {
                string? time = configuration.GetSection(_sectionName).Value;
                if (!string.IsNullOrWhiteSpace(time))
                {
                    bool parse = TimeSpan.TryParse(time, out TimeSpan readingCsvTime);
                    if (!parse)
                    {
                        throw new ParseException();
                    }
                    readingCsvTotalMinutes = Math.Floor(readingCsvTime.TotalMinutes);
                }
                else
                {
                    throw new EmptyConfigurationSectionException(_sectionName);
                }

                while (!stoppingToken.IsCancellationRequested)
                {
                    TimeSpan timeNow = DateTime.Now.ToUniversalTime().AddHours(GMT_OFFSET).TimeOfDay;

                    if (Convert.ToInt32(Math.Floor(timeNow.TotalMinutes)) == Convert.ToInt32(readingCsvTotalMinutes))
                    {
                        context.Copy();
                    }

                    // context.Copy();
                    // await Task.Delay(600000);
                    await Task.Delay(60000);
                }
            }
            catch (ParseException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (EmptyConfigurationSectionException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
