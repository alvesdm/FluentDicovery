using System;
using System.Linq;
using System.Net;

namespace FluentDicovery.ServiceDiscovery
{
    public class ServiceDisvoveryOptions
    {
        private string _serviceName = string.Empty;
        public string ServiceName {
            get { return _serviceName; }
            set { _serviceName = value.ToLower(); }
        }

        public ConsulOptions Consul { get; set; }

        public string HealthCheckTemplate { get; set; }

        public string[] Endpoints { get; set; }
    }

    public class ConsulOptions
    {
        public string HttpEndpoint { get; set; }

        public DnsEndpoint DnsEndpoint { get; set; }
    }

    public class DnsEndpoint
    {
        public string Address { get; set; }

        public int Port { get; set; }

        public IPEndPoint ToIPEndPoint()
        {
            return new IPEndPoint(IPAddress.Parse(Address), Port);
        }
    }
}