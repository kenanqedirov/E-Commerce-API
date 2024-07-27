using E_Commerce_API.Application.Repository;
using E_Commerce_API.Domain.Entities;
using E_Commerce_API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.Persistence.Repository.File
{
    public class FileReadRepository : ReadRepository<E_Commerce_API.Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(E_Commerce_APIDbContext context) : base(context)
        {
           
        }     
    }
}
