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

            if (_dbService.CheckUssrPassportFormat(series, number))
            {
                UssrPassport? passport = _dbService.GetUssrPassport(series, number);

                if (passport != null)
                {
                    return new OkObjectResult(passport);
                }
            }

            if (_dbService.CheckPassportFormat(series, number))
            {
                Passport? passport = _dbService.GetPassport(series, number);

                if (passport != null)
                {
                    return new OkObjectResult(passport);
                }
            }

            return NotFound();
        }

        [HttpGet]
        [Route("GetPassportHistory")]
        public IActionResult GetPassportHistory(string? series, string? number)
        {
            if (series == null || number == null)
            {
                return BadRequest();
            }

            if (_dbService.CheckUssrPassportFormat(series, number))
            {
                UssrPassport? passport = _dbService.GetUssrPassport(series, number);

                if (passport != null)
                {
                    var passportHistory = _dbService.GetUssrPassportHistory(passport);
                    return new OkObjectResult(passportHistory);
                }
            }

            if (_dbService.CheckPassportFormat(series, number))
            {
                Passport? passport = _dbService.GetPassport(series, number);

                if (passport != null)
                {
                    var passportHistory = _dbService.GetPassportHistory(passport);
                    return new OkObjectResult(passportHistory);
                }
            }

            return NotFound();
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

            var passportsHistories = _dbService.GetPassportsHistoriesByDate(startDate, endDate).Concat(_dbService.GetUssrPassportsHistoriesByDate(startDate, endDate));

            return new OkObjectResult(passportsHistories);
        }
    }
}
