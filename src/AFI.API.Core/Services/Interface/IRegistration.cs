using AFI.Data.Entities;
using System.Threading.Tasks;

namespace AFI.API.Core.Services.Interface
{
    public interface IRegistration
    {
        Task<bool> IsPolicyAlreadyRegistered(CustomerEntity customer);
    }
}
