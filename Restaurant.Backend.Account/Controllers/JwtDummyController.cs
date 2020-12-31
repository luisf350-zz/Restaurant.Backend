using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Backend.Common.Base;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Restaurant.Backend.CommonApi.Utils;

namespace Restaurant.Backend.Account.Controllers
{
    [AllowAnonymous]
    public class JwtDummyController : BaseController
    {
        private readonly IConfiguration _config;

        public JwtDummyController(ILogger<CustomerController> logger, IConfiguration config) : base(logger)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> GetToken()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, $"{Guid.NewGuid()}")
            };

            return Ok(new
            {
                token = JwtCreation.CreateJwtToken(claims, _config.GetSection("AppSettings:Token").Value)
            });
        }
    }
}
