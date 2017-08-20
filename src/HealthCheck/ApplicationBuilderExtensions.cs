using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentDicovery.HealthCheck
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
