using AFI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AFI.API.Core.Services.Interface
{
    public interface IRegistration
    {
        Task<ActionResult<int>> Register(CustomerEntity customer);
    }
}
