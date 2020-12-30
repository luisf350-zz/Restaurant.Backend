using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Backend.Common.Base;

namespace Restaurant.Backend.Account.Controllers
{
    public class CompanyController : BaseController
    {
        public CompanyController(ILogger<CustomerController> logger) : base(logger)
        {

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Working from Company Controller");
        }
    }
}
