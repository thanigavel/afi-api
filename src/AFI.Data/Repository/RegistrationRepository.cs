using AFI.Data.Context;
using AFI.Data.Entities;
using AFI.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AFI.Data.Repository
{
    public class RegistrationRepository : BaseRepository<CustomerEntity>, IRegistrationRepository
    {
        public RegistrationRepository(AFIContext context) : base(context)
        {
        }
        public async Task<bool> IsPolicyAlreadyRegistered(CustomerEntity customer)
        {
            var count = _context.Customer.CountAsync(x => x.PolicyReference == customer.PolicyReference);

            return (await count) > 0;
        }
    }
}
