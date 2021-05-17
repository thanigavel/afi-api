using AFI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AFI.Data.Context
{
    public class AFIContext : DbContext
    {
        private IConfiguration _config;

        public AFIContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("SQLDataBaseConnection"));
        }

        public DbSet<CustomerEntity> Customer { get; set; }
    }
}
