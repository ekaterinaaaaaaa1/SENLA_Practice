using Npgsql;
using Passports.Models;
using Passports.Services;
using System.Diagnostics;

namespace Passports.Converter
{
    public class DataConverterService : BackgroundService
    {
        private string _path = @"Converter\data1\Data1.csv";
        private readonly IConfiguration _configuration;

        public DataConverterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using StreamReader streamReader = new StreamReader(_path);
            Debug.WriteLine("ExecuteAsync is working");

            string? line = streamReader.ReadLine();
            while ((line = streamReader.ReadLine()) != null)
            {
                Passport? passport = CsvParserService.Parse(line);

                if (passport != null)
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();

                        using NpgsqlCommand command = new NpgsqlCommand("""
                            INSERT INTO inactivepassports (PASSP_SERIES, PASSP_NUMBER, ACTIVE)
                            VALUES (@s, @n, @a)
                            ON CONFLICT (PASSP_SERIES, PASSP_NUMBER)
                            DO UPDATE SET PASSP_SERIES = EXCLUDED.PASSP_SERIES, PASSP_NUMBER = EXCLUDED.PASSP_NUMBER;
                            """, connection);
                        command.Parameters.AddWithValue("s", passport.Series);
                        command.Parameters.AddWithValue("n", passport.Number);
                        command.Parameters.AddWithValue("a", passport.IsActive);
                        command.ExecuteNonQuery();
                    }
                }
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(200, stoppingToken);
            }

            await Task.CompletedTask;
        }
    }
}
