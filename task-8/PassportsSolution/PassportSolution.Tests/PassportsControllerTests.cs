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
        public void GetUssrPassportEmptyEnterReturnsBadRequest()
        {
            var mock = new Mock<IDBService>();

            string? series = null;
            string? number = null;

            var controller = new PassportsController(mock.Object);

            var result = controller.GetUssrPassport(series, number);
            Assert.IsType<BadRequestResult>(result);
        }
    }
}