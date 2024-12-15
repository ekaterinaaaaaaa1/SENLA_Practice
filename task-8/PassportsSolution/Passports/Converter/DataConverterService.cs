﻿using Passports.Services.Interfaces;
using Passports.Exceptions;

namespace Passports.Converter
{
    /// <summary>
    /// IHostedService for data conversion.
    /// </summary>
    /// <param name="serviceScopeFactory">A factory for creating service scope for the database service.</param>
    /// <param name="configuration">A set of key/value application configuration properties.</param>
    public class DataConverterService(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration) : BackgroundService
    {
        private readonly string _gmtOffsetSection = "GmtOffset";
        private readonly string _readingCsvTimeSection = "ReadingCsvTime";

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IDBService>();
            double readingCsvTotalMinutes = -1;

            try
            {
                string? time = configuration.GetSection(_readingCsvTimeSection).Value;
                if (string.IsNullOrWhiteSpace(time))
                {
                    throw new EmptyConfigurationSectionException(_readingCsvTimeSection);
                }
                if (!TimeSpan.TryParse(time, out TimeSpan readingCsvTime))
                {
                    throw new ParseException();
                }

                readingCsvTotalMinutes = Math.Floor(readingCsvTime.TotalMinutes);

                while (!stoppingToken.IsCancellationRequested)
                {
                    string? gmtOffsetValue = configuration.GetSection(_gmtOffsetSection).Value;
                    if (string.IsNullOrWhiteSpace(gmtOffsetValue))
                    {
                        throw new EmptyConfigurationSectionException(_gmtOffsetSection);
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
