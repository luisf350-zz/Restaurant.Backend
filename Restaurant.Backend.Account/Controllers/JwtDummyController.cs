using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Restaurant.Backend.CommonApi.Base;
using Restaurant.Backend.CommonApi.Utils;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Restaurant.Backend.Account.Controllers
{
    [AllowAnonymous]
    public class JwtDummyController : BaseController
    {
        private readonly IConfiguration _config;

        public JwtDummyController(ILogger<CustomerController> logger, IConfiguration config, IMapper mapper)
            : base(logger, mapper)
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
