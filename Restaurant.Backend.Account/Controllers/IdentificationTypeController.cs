using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Backend.Common.Constants;
using Restaurant.Backend.CommonApi.Base;
using Restaurant.Backend.Domain.Contract;
using Restaurant.Backend.Dto.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Backend.Account.Controllers
{
    public class IdentificationTypeController : BaseController
    {
        private readonly IIdentificationTypeDomain _identificationTypeDomain;

        public IdentificationTypeController(ILogger<IdentificationTypeController> logger, IIdentificationTypeDomain identificationTypeDomain, IMapper mapper)
            : base(logger, mapper)
        {
            _identificationTypeDomain = identificationTypeDomain;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var resultTypes = await _identificationTypeDomain.GetAll();

            return Ok(Mapper.Map<IList<IdentificationTypeDto>>(resultTypes));
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var resultType = await _identificationTypeDomain.Find(id);

            if (resultType == null)
            {
                return NotFound(string.Format(Constants.NotFound, id));
            }

            return Ok(Mapper.Map<IdentificationTypeDto>(resultType));
        }
    }
}
