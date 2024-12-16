using Microsoft.AspNetCore.Mvc;
using Moq;
using Passports.Controllers;
using Passports.Services.Interfaces;

namespace PassportSolution.Tests
{
    public class PassportsControllerTests
    {
        [Fact]
        public void GetPassportEmptyEnterReturnsBadRequest()
        {
            var mock = new Mock<IDBService>();

            string? series = null;
            string? number = null;

            var controller = new PassportsController(mock.Object);

            var result = controller.GetPassport(series, number);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetPassportInvalidSeriesReturnsNotFound()
        {
            var mock = new Mock<IDBService>();

            string? series = "ABC-до";
            string? number = "147853";

            var controller = new PassportsController(mock.Object);

            var result = controller.GetPassport(series, number);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetPassportInvalidNumberReturnsNotFound()
        {
            var mock = new Mock<IDBService>();

            string? series = "XX-до";
            string? number = "1473";

            var controller = new PassportsController(mock.Object);

            var result = controller.GetPassport(series, number);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetPassportHistoryEmptySeriesEnterReturnsBadRequest()
        {
            var mock = new Mock<IDBService>();

            string? series = null;
            string? number = "147853";

            var controller = new PassportsController(mock.Object);

            var result = controller.GetPassportHistory(series, number);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetPassportsHistoriesByDateEmptyStartDayEnterReturnsBadRequest()
        {
            var mock = new Mock<IDBService>();

            int? startYear = 2024;
            int? startMonth = 9;
            int? startDay = null;
            int? endYear = 2024;
            int? endMonth = 12;
            int? endDay = 15;

            var controller = new PassportsController(mock.Object);

            var result = controller.GetPassportsHistoriesByDate(startYear, startMonth, startDay, endYear, endMonth, endDay);
            Assert.IsType<BadRequestResult>(result);
        }
    }
}