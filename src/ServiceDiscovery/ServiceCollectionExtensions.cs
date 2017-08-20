using System;
using System.Net;
using Consul;
using DnsClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.HealthChecks;
using FluentDicovery.HealthCheck;

namespace FluentDicovery.ServiceDiscovery
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services, IConfiguration serviceOptionsConfiguration)
        {
            services.AddOptions();

            // setup options
            services.Configure<ServiceDisvoveryOptions>(serviceOptionsConfiguration);

            // register consul client
            services.AddSingleton<IConsulClient>(p => new ConsulClient(cfg =>
            {
                var serviceConfiguration = p.GetRequiredService<IOptions<ServiceDisvoveryOptions>>().Value;

                if (!string.IsNullOrEmpty(serviceConfiguration.Consul.HttpEndpoint))
                {
                    // if not configured, the client will use the default value "127.0.0.1:8500"
                    cfg.Address = new Uri(serviceConfiguration.Consul.HttpEndpoint);
                }
            }));

            // register dns lookup
            services.AddSingleton<IDnsQuery>(p =>
            {
                var serviceConfiguration = p.GetRequiredService<IOptions<ServiceDisvoveryOptions>>().Value;

                if (serviceConfiguration.Consul.DnsEndpoint == null)
                    throw new Exception("ServiceDiscovery:Consul:DnsEndpoint setting is missing.");

                var client = new LookupClient(serviceConfiguration.Consul.DnsEndpoint.ToIPEndPoint());

                client.EnableAuditTrail = false;
                client.UseCache = true;
                client.MinimumCacheTimeout = TimeSpan.FromSeconds(1);
                return client;
            });

            return services;
        }

        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services, IConfigurationRoot configuration)
            => AddServiceDiscovery(services, configuration, null);

        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services, IConfigurationRoot configuration, Action<HealthCheckBuilder> builder)
        {
            services.AddHealthCheck(builder);

            return AddServiceDiscovery(services, configuration.GetSection("ServiceDiscovery"));
        }

        public static IServiceCollection AddServiceDiscoveryHelper(this IServiceCollection services, string consulEndPoint = "127.0.0.1", int consulPort = 8600)
        {
            services.AddSingleton<IDnsQuery>(new LookupClient(IPAddress.Parse(consulEndPoint), consulPort));
            services.AddSingleton<DiscoveryHelper>();

            return services;
        }
    }
}