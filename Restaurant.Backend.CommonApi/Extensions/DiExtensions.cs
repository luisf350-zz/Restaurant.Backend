using Microsoft.Extensions.DependencyInjection;
using Restaurant.Backend.Common.Enums;
using Restaurant.Backend.Domain.Contract;
using Restaurant.Backend.Domain.Implementation;
using Restaurant.Backend.Repositories.Repositories;

namespace Restaurant.Backend.CommonApi.Extensions
{
    public static class DiExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, Microservice service)
        {
            switch (service)
            {
                case Microservice.Account:
                    services.AddScoped<IIdentificationTypeDomain, IdentificationTypeDomain>();
                    services.AddScoped<IIdentificationTypeRepository, IdentificationTypeRepository>();
                    break;
            }
        }
    }
}
