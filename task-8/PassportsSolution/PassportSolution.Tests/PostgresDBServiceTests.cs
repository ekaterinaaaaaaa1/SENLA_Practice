using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Passports.Database;
using Passports.Models;
using Passports.Services;

namespace Passports.Tests
{
    public class PostgresDBServiceTests
    {
        [Fact]
        public void GetPassportInvalidSeriesReturnsNull()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(c => c.GetSection("GmtOffset").Value).Returns("3");
            DbContextOptions<ApplicationContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>().UseNpgsql("Host=localhost;Port=5433;Database=passports_db;Username=postgres;Password=p2658;CommandTimeout=0").Options;
            var mockApplicationContext = new Mock<ApplicationContext>(dbContextOptions);
            var service = new PostgresDBService(mockApplicationContext.Object, mockConfiguration.Object);

            string? series = "000";
            string? number = "122333";

            var result = service.CheckPassportFormat(series, number);
            Assert.False(result);
        }

        [Fact]
        public void GetUssrPassportInvalidNumberReturnsNull()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(c => c.GetSection("GmtOffset").Value).Returns("3");
            DbContextOptions<ApplicationContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>().UseNpgsql("Host=localhost;Port=5433;Database=passports_db;Username=postgres;Password=p2658;CommandTimeout=0").Options;
            var mockApplicationContext = new Mock<ApplicationContext>(dbContextOptions);
            var service = new PostgresDBService(mockApplicationContext.Object, mockConfiguration.Object);

            string? series = "XX-БО";
            string? number = "122333";

            var result = service.CheckUssrPassportFormat(series, number);
            Assert.True(result);
        }
    }
}
