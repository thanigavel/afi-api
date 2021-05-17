using AFI.API.Core.ServiceModels;
using System.Threading.Tasks;

namespace AFI.API.Core.Services.Interface
{
    public interface IRegistration
    {
        Task<Outcome<int>> Register(CustomerServiceModel customer);
    }
}
