using E_Commerce_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.Application.Abstraction
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
