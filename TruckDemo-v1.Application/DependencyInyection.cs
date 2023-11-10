using Microsoft.Extensions.DependencyInjection;

using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo_v1.Application
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(Result).Assembly));

            return services;
        }
    }
}