using E_Commerce_API.Application.Repository;
using E_Commerce_API.Domain.Entities;
using E_Commerce_API.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.Persistence.Repository
{
    public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteCustomer
    {
        public CustomerWriteRepository(E_Commerce_APIDbContext context) : base(context)
        {
        }
    }
}
