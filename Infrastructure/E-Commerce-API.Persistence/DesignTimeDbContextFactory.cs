using E_Commerce_API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace E_Commerce_API.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<E_Commerce_APIDbContext>
    {
        public E_Commerce_APIDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<E_Commerce_APIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
