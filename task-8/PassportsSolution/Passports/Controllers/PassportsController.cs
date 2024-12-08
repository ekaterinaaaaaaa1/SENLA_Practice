using Microsoft.AspNetCore.Mvc;
using Passports.Models;
using Passports.Services.Interfaces;

namespace Passports.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PassportsController : ControllerBase
    {
        private readonly IDBService _dbService;

        public PassportsController(IDBService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        [Route("GetPassport")]
        public IActionResult GetPassport(string? series, string? number)
        {
            if (series == null || number == null)
            {
                return BadRequest();
            }
            
            Passport? passport = _dbService.GetPassport(series, number);
            
            if (passport == null)
            {
                return NotFound();
            }

            return new OkObjectResult(passport);
        }

        [HttpGet]
        [Route("GetUssrPassport")]
        public IActionResult GetUssrPassport(string? series, string? number)
        {
            if (series == null || number == null)
            {
                return BadRequest();
            }

            UssrPassport? passport = _dbService.GetUssrPassport(series, number);

            if (passport == null)
            {
                return NotFound();
            }

            return new OkObjectResult(passport);
        }

        [HttpGet]
        [Route("GetPassportHistory")]
        public IActionResult GetPassportHistory(string? series, string? number)
        {
            if (series == null || number == null)
            {
                return BadRequest();
            }

            Passport? passport = _dbService.GetPassport(series, number);

            if (passport == null)
            {
                return NotFound();
            }

            var passportHistory = _dbService.GetPassportHistory(passport);

            return new OkObjectResult(passportHistory);
        }

        [HttpGet]
        [Route("GetUssrPassportHistory")]
        public IActionResult GetUssrPassportHistory(string? series, string? number)
        {
            if (series == null || number == null)
            {
                return BadRequest();
            }

            UssrPassport? passport = _dbService.GetUssrPassport(series, number);

            if (passport == null)
            {
                return NotFound();
            }

            var passportHistory = _dbService.GetUssrPassportHistory(passport);

            return new OkObjectResult(passportHistory);
        }

        [HttpGet]
        [Route("GetPassportsHistoriesByDate")]
        public IActionResult GetPassportsHistoriesByDate(int? startYear, int? startMonth, int? startDay, int? endYear, int? endMonth, int? endDay)
        {
            if (startYear == null || startMonth == null || startDay == null || endYear == null || endMonth == null || endDay == null)
            {
                return BadRequest();
            }
            
            DateOnly startDate = new DateOnly((int)startYear, (int)startMonth, (int)startDay);
            DateOnly endDate = new DateOnly((int)endYear, (int)endMonth, (int)endDay);

            var passportsHistories = _dbService.GetPassportsHistoriesByDate(startDate, endDate);

            return new OkObjectResult(passportsHistories);
        }

        [HttpGet]
        [Route("GetUssrPassportsHistoriesByDate")]
        public IActionResult GetUssrPassportsHistoriesByDate(int? startYear, int? startMonth, int? startDay, int? endYear, int? endMonth, int? endDay)
        {
            if (startYear == null || startMonth == null || startDay == null || endYear == null || endMonth == null || endDay == null)
            {
                return BadRequest();
            }

            DateOnly startDate = new DateOnly((int)startYear, (int)startMonth, (int)startDay);
            DateOnly endDate = new DateOnly((int)endYear, (int)endMonth, (int)endDay);

            var passportsHistories = _dbService.GetUssrPassportsHistoriesByDate(startDate, endDate);

            return new OkObjectResult(passportsHistories);
        }
    }
}
