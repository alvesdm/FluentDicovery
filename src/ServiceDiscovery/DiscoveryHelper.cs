using DnsClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FluentDicovery.ServiceDiscovery
{
    public class DiscoveryHelper
    {
        private IDnsQuery _dnsResolver;

        public DiscoveryHelper(IDnsQuery dnsResolver)
        {
            _dnsResolver = dnsResolver;
        }

        public async Task<List<ServiceHostEntry>> ResolveThisService(string serviceName, string baseDomain = "service.consul") {
            var list = await _dnsResolver.ResolveServiceAsync(baseDomain.ToLower(), serviceName.ToLower());

            return list.ToList();
        }
    }
}
