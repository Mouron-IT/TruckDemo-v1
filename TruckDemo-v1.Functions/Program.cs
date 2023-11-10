using Microsoft.Extensions.Hosting;

using TruckDemo_v1.Application;
using TruckDemo_v1.Infraestructure;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()

    .ConfigureServices((context, services) =>
    {
        services.AddInfraestructure(context.Configuration);
        services.AddApplication();

    })
    .Build();

host.Run();
