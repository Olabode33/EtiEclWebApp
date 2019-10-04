using System.Collections.Generic;

namespace TestDemo.MultiTenancy.HostDashboard.Dto
{
    public class GetExpiringTenantsOutput
    {
        public List<ExpiringTenant> ExpiringTenants { get; set; }

        public GetExpiringTenantsOutput(List<ExpiringTenant> expiringTenants)
        {
            ExpiringTenants = expiringTenants;
        }
    }
}