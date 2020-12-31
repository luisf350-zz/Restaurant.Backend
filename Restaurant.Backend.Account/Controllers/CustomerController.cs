using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Restaurant.Backend.Common.Base;
using Restaurant.Backend.CommonApi.Utils;
using Restaurant.Backend.Dto.Account;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Restaurant.Backend.Account.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IConfiguration _config;

        public CustomerController(ILogger<CustomerController> logger, IConfiguration config) : base(logger)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(Ok("Working from Company Controller"));
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(CustomerLoginDto login)
        {
            //var userFromRepo = await repo.Login(userForLogin.UserName, userForLogin.Password);

            //if (userFromRepo == null)
            //{
            //    return Unauthorized();
            //}

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
