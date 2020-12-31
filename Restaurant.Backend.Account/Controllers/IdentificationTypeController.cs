using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Backend.CommonApi.Base;
using Restaurant.Backend.Domain.Contract;

namespace Restaurant.Backend.Account.Controllers
{
    public class IdentificationTypeController : BaseController
    {
        private readonly IIdentificationTypeDomain _identificationTypeDomain;

        public IdentificationTypeController(ILogger<IdentificationTypeController> logger, IIdentificationTypeDomain identificationTypeDomain) : base(logger)
        {
            _identificationTypeDomain = identificationTypeDomain;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var resultTypes = await _identificationTypeDomain.GetAll();

            return Ok(resultTypes);
        }
    }
}
