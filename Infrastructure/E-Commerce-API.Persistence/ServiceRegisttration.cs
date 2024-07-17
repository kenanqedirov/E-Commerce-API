using E_Commerce_API.Persistence.Contexts;
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
        }
    }
}
