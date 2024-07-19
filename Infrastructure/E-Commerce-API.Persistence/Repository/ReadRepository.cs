using E_Commerce_API.Application.Repository;
using E_Commerce_API.Domain.Entities.Common;
using E_Commerce_API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.Persistence.Repository
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly E_Commerce_APIDbContext _context;
        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll() 
            => Table;
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
            =>Table.Where(method);
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
            =>await Table.FirstOrDefaultAsync(method);

        public async Task<T> GetByIdAsync(string id)
           =>await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
        
    }
}
