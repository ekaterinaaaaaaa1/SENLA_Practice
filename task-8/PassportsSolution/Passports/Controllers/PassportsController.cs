using Microsoft.AspNetCore.Mvc;
using Passports.Database;
using Passports.Models;

namespace Passports.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PassportsController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;

        public PassportsController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpGet]
        public IActionResult GetPassport(int id)
        {
            Passport? passport = _applicationContext.InactivePassports.Find(id);

            if (passport == null)
            {
                return NotFound();
            }

            return new OkObjectResult(passport);
        }
    }
}
