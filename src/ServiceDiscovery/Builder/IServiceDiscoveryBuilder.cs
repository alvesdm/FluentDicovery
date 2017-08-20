using Microsoft.Extensions.HealthChecks;

namespace FluentDicovery.ServiceDiscovery.Builder
{
    public interface IServiceDiscoveryBuilder
    {
        IServiceDiscoveryBuilder WithCheck(IHealthCheck check);
    }
}