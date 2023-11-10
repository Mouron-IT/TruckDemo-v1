using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using TruckDemo_v1.Application.Data;
using TruckDemo_v1.Application.Managers;
using TruckDemo_v1.Domain.Entities.Identity;
using TruckDemo_v1.Domain.Enum;
using TruckDemo_v1.Infraestructure.Data;
using TruckDemo_v1.Infraestructure.Managers;

namespace TruckDemo_v1.Infraestructure
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ITruckDemoContext, TruckDemoContext>
                (o => o.UseSqlServer(configuration["SqlServerConnectionString"]));

            services.AddIdentity<ApplicationUser,Role>(options => options.SignIn.RequireConfirmedAccount = true)            
            .AddEntityFrameworkStores<TruckDemoContext>().AddDefaultTokenProviders();
            services.AddScoped<IJwtManager, JwtManager>();

            return services;
        }
    }
}
