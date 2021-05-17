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

        [HttpPost]
        public async Task<ActionResult<int>> Register( Customer customerModel)
        {
            try
            {
                CustomerEntity customer = _mapper.Map<CustomerEntity>(customerModel);

                return await _registration.Register(customer);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error Occurred", ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error Occurred");
            }
        }
    }
}
