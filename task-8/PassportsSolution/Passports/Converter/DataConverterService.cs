using Passports.Services.Interfaces;
using Passports.Exceptions;
using Passports.Options;
using Microsoft.Extensions.Options;

namespace Passports.Converter
{
    /// <summary>
    /// IHostedService for data conversion.
    /// </summary>
    /// <param name="serviceScopeFactory">A factory for creating service scope for the database service.</param>
    /// <param name="options">AppSettings section values.</param>
    public class DataConverterService(IServiceScopeFactory serviceScopeFactory, IOptions<AppSettings> options) : BackgroundService
    {
        private AppSettings? _appSettings;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IDBService>();
            double readingCsvTotalMinutes = -1;

            try
            {
                _appSettings = options.Value;
                string? time = _appSettings.ReadingCsvTime;
                if (string.IsNullOrWhiteSpace(time))
                {
                    throw new EmptyConfigurationSectionException(_appSettings.ReadingCsvTime.GetType().Name);
                }
                if (!TimeSpan.TryParse(time, out TimeSpan readingCsvTime))
                {
                    throw new ParseException();
                }

                readingCsvTotalMinutes = Math.Floor(readingCsvTime.TotalMinutes);

                while (!stoppingToken.IsCancellationRequested)
                {
                    string? gmtOffsetValue = _appSettings.GmtOffset;
                    if (string.IsNullOrWhiteSpace(gmtOffsetValue))
                    {
                        throw new EmptyConfigurationSectionException(_appSettings.GmtOffset.GetType().Name);
                    }
                    if (!int.TryParse(gmtOffsetValue, out int gmtOffset))
                    {
                        throw new ParseException();
                    }

                    TimeSpan timeNow = DateTime.Now.ToUniversalTime().AddHours(gmtOffset).TimeOfDay;

                    if (Convert.ToInt32(Math.Floor(timeNow.TotalMinutes)) == Convert.ToInt32(readingCsvTotalMinutes))
                    {
                        context.Copy();
                    }
                    
                    await Task.Delay(60000);
                }
            }
            catch (Exception ex) when (ex is ParseException || ex is EmptyConfigurationSectionException)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
