using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Restaurant.Backend.Common.Constants;
using Restaurant.Backend.CommonApi.Base;
using Restaurant.Backend.CommonApi.Utils;
using Restaurant.Backend.Domain.Contract;
using Restaurant.Backend.Dto.Account;
using Restaurant.Backend.Dto.Entities;
using Restaurant.Backend.Entities.Entities;
using System.Security.Claims;
using System.Threading.Tasks;
using Restaurant.Backend.Common.Utils;

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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var resultTypes = await _customerDomain.GetAll(null, null, x => x.IdentificationType);

            return Ok(Mapper.Map<IList<CustomerDto>>(resultTypes));
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var resultType = await _customerDomain.Find(id);

            return resultType == null ?
                (IActionResult)NotFound(string.Format(Constants.NotFound, id))
                : Ok(Mapper.Map<CustomerDto>(resultType));
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(CustomerLoginDto login)
        {
            var userFromRepo = await _customerDomain.Login(login.Email, login.Password);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, $"{userFromRepo.Id}"),
                new Claim(ClaimTypes.Name, $"{userFromRepo.FirstName} {userFromRepo.LastName}")
            };

            return Ok(new
            {
                token = await JwtCreationUtil.CreateJwtToken(claims, _config)
            });
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CustomerDto customerDto)
        {
            var customer = Mapper.Map<Customer>(customerDto);

            PasswordUtils.CreatePasswordHash(customerDto.Password, out var passwordHash, out var passwordSalt);
            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;

            var result = await _customerDomain.Create(customer);

            return result == 0 ?
                (IActionResult)BadRequest(Constants.OperationNotCompleted)
                : Ok(customerDto);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(CustomerDto modelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Constants.ModelNotValid);
            }

            var model = await _customerDomain.Find(modelDto.Id);
            if (model == null)
            {
                return NotFound(string.Format(Constants.NotFound, modelDto.Id));
            }

            model.IdentificationTypeId = modelDto.IdentificationTypeId;
            model.IdentificationNumber = modelDto.IdentificationNumber;
            model.FirstName = modelDto.FirstName;
            model.LastName = modelDto.LastName;
            model.Birthday = modelDto.Birthday;
            model.Gender = modelDto.Gender;

            var result = await _customerDomain.Update(model);

            return result ?
                Ok(Mapper.Map<CustomerDto>(model))
                : (IActionResult)BadRequest(Constants.OperationNotCompleted);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(CustomerDto modelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Constants.ModelNotValid);
            }

            var model = await _customerDomain.Find(modelDto.Id);
            if (model == null)
            {
                return NotFound(string.Format(Constants.NotFound, modelDto.Id));
            }

            var result = await _customerDomain.Delete(model);

            return result == 1 ?
                Ok(Mapper.Map<CustomerDto>(model))
                : (IActionResult)BadRequest(Constants.OperationNotCompleted);
        }
    }
}
