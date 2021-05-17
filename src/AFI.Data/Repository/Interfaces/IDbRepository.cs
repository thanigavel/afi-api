using System.Linq;
using System.Threading.Tasks;

namespace AFI.Data.Repository.Interfaces
{
    public interface IDbRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
