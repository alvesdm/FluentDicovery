using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentDicovery.ServiceDiscovery
{
    public static class HealthCheckWebHostBuilderExtension
    {
        public static IWebHostBuilder UseHealthChecks(this IWebHostBuilder builder)
         => Microsoft.AspNetCore.Hosting.HealthCheckWebHostBuilderExtension.UseHealthChecks(builder, "/health");
    }
}
