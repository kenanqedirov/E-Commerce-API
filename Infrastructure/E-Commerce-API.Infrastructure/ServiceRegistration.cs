using E_Commerce_API.Application.Abstraction.Storage;
using E_Commerce_API.Infrastructure.Enums;
using E_Commerce_API.Infrastructure.Services.Storage;
using E_Commerce_API.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce_API.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService,StorageService>();

        }
        public static void AddStorage<T>(this IServiceCollection services) where T : class , IStorage
        {
            services.AddScoped<IStorage ,T>();
        }
        public static void AddStorage<T>(this IServiceCollection services,StorageType storageType) 
        {
            switch (storageType)
            {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:

                    break;
                case StorageType.AWS:

                    break;
                default:
                    services.AddScoped<IStorage,LocalStorage>();
                    break;
            }
        }
    }
}
