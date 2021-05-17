using AFI.API.Core.Services.Interface;
using AFI.API.Model;
using Microsoft.AspNetCore.Mvc;
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

        public RegistrationController(IRegistration registration)
        {
            _registration = registration;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Register( Customer customerModel)
        {
            return null;
        }
    }
}
