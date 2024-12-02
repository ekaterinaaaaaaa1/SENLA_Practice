using Microsoft.AspNetCore.Mvc;
using Passports.Models;
using Passports.Models.DTO;
using Passports.Models.JsonConverters;
using Passports.Services.Interfaces;
using System.ComponentModel;
using Newtonsoft.Json;

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
        public IActionResult GetPassport(short series, int number)
        {
            Passport? passport = _dbService.GetPassport(series, number);
            
            if (passport == null)
            {
                return NotFound();
            }

            return new OkObjectResult(passport);
        }

        [HttpGet]
        [Route("GetUssrPassport")]
        public IActionResult GetUssrPassport(string series, int number)
        {
            UssrPassport? passport = _dbService.GetUssrPassport(series, number);

            if (passport == null)
            {
                return NotFound();
            }

            return new OkObjectResult(passport);
        }

        [HttpGet]
        [Route("GetPassportHistory")]
        public IActionResult GetPassportHistory(short series, int number)
        {
            Passport? passport = _dbService.GetPassport(series, number);

            if (passport == null)
            {
                return NotFound();
            }

            List<PassportChanges> passportHistory = _dbService.GetPassportHistory(passport);

            return new OkObjectResult(passportHistory);
        }

        [HttpGet]
        [Route("GetUssrPassportHistory")]
        public IActionResult GetUssrPassportHistory(string series, int number)
        {
            UssrPassport? passport = _dbService.GetUssrPassport(series, number);

            if (passport == null)
            {
                return NotFound();
            }

            List<PassportChanges> passportHistory = _dbService.GetUssrPassportHistory(passport);

            return new OkObjectResult(passportHistory);
        }

        [HttpGet]
        [Route("GetPassportsHistoriesByDate")]
        public IActionResult GetPassportsHistoriesByDate(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            DateOnly startDate = new DateOnly(startYear, startMonth, startDay);
            DateOnly endDate = new DateOnly(endYear, endMonth, endDay);

            List<KeyValuePair<PassportOnly, List<PassportChanges>>> passportsHistories = _dbService.GetPassportsHistoriesByDate(startDate, endDate);

            return new OkObjectResult(passportsHistories);
        }

        [HttpGet]
        [Route("GetUssrPassportsHistoriesByDate")]
        public IActionResult GetUssrPassportsHistoriesByDate(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            DateOnly startDate = new DateOnly(startYear, startMonth, startDay);
            DateOnly endDate = new DateOnly(endYear, endMonth, endDay);

            List<KeyValuePair<UssrPassportOnly, List<PassportChanges>>> passportsHistories = _dbService.GetUssrPassportsHistoriesByDate(startDate, endDate);

            return new OkObjectResult(passportsHistories);
        }
    }
}
