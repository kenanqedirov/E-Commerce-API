using E_Commerce_API.Application.Repository;
using E_Commerce_API.Domain.Entities;
using E_Commerce_API.Persistence.Contexts;
using E_Commerce_API.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.Persistence
{
    public static class ServiceRegisttration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
         services.AddDbContext<E_Commerce_APIDbContext>(options =>
         {
             options.UseSqlServer(Configuration.ConnectionString);
         });
            services.AddScoped<ICustomerReadRepository,CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository,CustomerWriteRepository>();
            services.AddScoped<IProductReadRepository,ProductReadRepository>();
            services.AddScoped<IProductWriteRepository,ProductWriteRepository>();
            services.AddScoped<IOrderReadRepository,OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository,OrderWriteRepository>(); 
        }
    }
}
