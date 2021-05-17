using AFI.API.Core.ServiceModels;
using AFI.API.Core.Services.Interface;
using AFI.API.Model;
using AFI.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFI.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private IRegistration _registration;
        private IMapper _mapper;
        private ILogger _logger;

        public RegistrationController(IRegistration registration, IMapper mapper, ILogger logger)
        {
            _registration = registration;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Use this endpoint to register customer with policy reference number
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        /// <response code="200">Customer is registered with policy reference</response>
        /// <response code="503">Request has not processed</response>
        /// <response code="400">Policy has already registered</response>
        /// <response code="500">Error Occurred</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Register(Customer customerModel)
        {
            try
            {
                var customer = _mapper.Map<CustomerServiceModel>(customerModel);

                var result =  await _registration.Register(customer);

                if (result == null)  return this.StatusCode(StatusCodes.Status500InternalServerError, "Error Occurred");

                if (result.Successful) return Ok(result.Result);

                return BadRequest(result.ErrorMessage);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error Occurred", ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error Occurred");
            }
        }
    }
}
