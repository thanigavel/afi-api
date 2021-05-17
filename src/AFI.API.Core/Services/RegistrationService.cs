using AFI.API.Core.Services.Interface;
using AFI.Data.Entities;
using System.Threading.Tasks;

namespace AFI.API.Core.Services
{
    public class RegistrationService : IRegistration
    {
        public Task<bool> IsPolicyAlreadyRegistered(CustomerEntity customer)
        {
            throw new System.NotImplementedException();
        }
    }
}
