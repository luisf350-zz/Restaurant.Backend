using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Backend.Common.Base;
using System.Threading.Tasks;

namespace Restaurant.Backend.Account.Controllers
{
    public class CustomerController : BaseController
    {
        public CustomerController(ILogger<CustomerController> logger) : base(logger)
        {

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Working from Customer Controller");
        }
    }
}
