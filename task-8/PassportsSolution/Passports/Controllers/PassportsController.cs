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
        public IActionResult GetPassport(short series, int number)
        {
            Passport? passport = _dbService.GetPassport(series, number);
            
            if (passport == null)
            {
                return NotFound();
            }

            return new OkObjectResult(passport);
        }
    }
}
