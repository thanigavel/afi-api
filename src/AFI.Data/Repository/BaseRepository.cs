using AFI.Data.Context;
using AFI.Data.Repository.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AFI.Data.Repository
{
    public class BaseRepository<TEntity> : IDbRepository<TEntity> where TEntity : class, new()
    {
        protected AFIContext _context;
        public BaseRepository(AFIContext context )
        {
            _context = context;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");

            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return _context.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");

            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }
    }
}
