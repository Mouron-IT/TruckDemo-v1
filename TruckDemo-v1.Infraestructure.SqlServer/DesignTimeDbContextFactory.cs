using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using TruckDemo_v1.Infrastructure.Data;

namespace TruckDemo_v1.Infraestructure.SqlServer
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TruckDemoContext>
    {
        public TruckDemoContext CreateDbContext(string[] args)
        {
            //https://stackoverflow.com/a/47452555
            //$env:ASPNETCORE_ENVIRONMENT='Development'


            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../TruckDemo-v1.Functions"))
                .AddEnvironmentVariables()
                .AddJsonFile($"local.settings.json", true);


            IConfigurationRoot configuration = configurationBuilder.Build();

            var builder = new DbContextOptionsBuilder<TruckDemoContext>();

            var connectionString = configuration["SqlServerConnectionString"];

            if (!string.IsNullOrWhiteSpace(envName))
            {
                connectionString = configuration["Values:SqlServerConnectionString"];
            }

            builder.UseSqlServer(connectionString!, o => o.MigrationsAssembly(typeof(DesignTimeDbContextFactory).Assembly.GetName().Name));

            return new TruckDemoContext(builder.Options);
        }
    }
}