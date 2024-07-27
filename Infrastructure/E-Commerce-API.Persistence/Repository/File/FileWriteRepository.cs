using E_Commerce_API.Application.Repository;
using E_Commerce_API.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.Persistence.Repository.File
{
    public class FileWriteRepository : WriteRepository<E_Commerce_API.Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(E_Commerce_APIDbContext context) : base(context)
        {
        }
        
    }
}
