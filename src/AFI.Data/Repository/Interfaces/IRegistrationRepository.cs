using AFI.Data.Entities;
using System.Threading.Tasks;

namespace AFI.Data.Repository.Interfaces
{
    public interface IRegistrationRepository : IDbRepository<CustomerEntity>
    {
        Task<bool> IsPolicyAlreadyRegistered(CustomerEntity customer);
    }
}
