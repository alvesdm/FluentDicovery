using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentDicovery.HealthCheck
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, Action<HealthCheckBuilder> builder)
        {
            if (builder == null)
                builder = new Action<HealthCheckBuilder>((x) => { });

            if (builder != null)
                services.AddHealthChecks(builder);

            return services;
        }
    }
}
