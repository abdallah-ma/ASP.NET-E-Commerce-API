
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DemoAPI.Common.MassTransit
{


    public static class MassTransitService
    {
        public static IServiceCollection AddMassTransitServiceWithRabbitMQ(this IServiceCollection services)
        {
            services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((Context, configurator) =>
                {
                    var Configuration = Context.GetService<IConfiguration>();
                    var serviceSettings = Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                    var rabbitMQSettings = Configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                    configurator.Host(rabbitMQSettings.Host);
                    configurator.ConfigureEndpoints(Context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));
                });



            });

            return services;
        }
    }

}