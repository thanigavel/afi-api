using AFI.API.Core.Services.Interface;
using AFI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AFI.API.Core.Services
{
    public class RegistrationService : IRegistration
    {
        Task<ActionResult<int>> IRegistration.Register(CustomerEntity customer)
        {
            throw new System.NotImplementedException();
        }
    }
}
