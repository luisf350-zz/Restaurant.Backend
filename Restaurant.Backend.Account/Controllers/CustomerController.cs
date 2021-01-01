using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Restaurant.Backend.CommonApi.Base;
using Restaurant.Backend.CommonApi.Utils;
using Restaurant.Backend.Dto.Account;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Restaurant.Backend.Common.Constants;
using Restaurant.Backend.Domain.Contract;
using Restaurant.Backend.Dto.Entities;
using Restaurant.Backend.Entities.Entities;

namespace Restaurant.Backend.Account.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly ICustomerDomain _customerDomain;

        public CustomerController(ILogger<CustomerController> logger, IConfiguration config, IMapper mapper, ICustomerDomain customerDomain)
            : base(logger, mapper)
        {
            _config = config;
            _customerDomain = customerDomain;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(Ok("Working from Company Controller"));
        }

        //[AllowAnonymous]
        //[HttpPost("Login")]
        //public async Task<IActionResult> Login(CustomerLoginDto login)
        //{
        //    var userFromRepo = await _customerDomain.Login(login.Email, login.Password);

        //    if (userFromRepo == null)
        //    {
        //        return Unauthorized();
        //    }

        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, $"{userFromRepo.Id}"),
        //        new Claim(ClaimTypes.Name, $"{userFromRepo.FirstName} {userFromRepo.LastName}")
        //    };

        //    return Ok(new
        //    {
        //        token = JwtCreationUtil.CreateJwtToken(claims, _config)
        //    });
        //}

        //[HttpPost("Create")]
        //public async Task<IActionResult> Create(CustomerDto customerDto)
        //{
        //    var customer = Mapper.Map<Customer>(customerDto);
        //    var result = await _customerDomain.Create(customer);

        //    return result == 0 ?
        //        (IActionResult)BadRequest(Constants.OperationNotCompleted)
        //        : Ok(customerDto);
        //}
    }
}
