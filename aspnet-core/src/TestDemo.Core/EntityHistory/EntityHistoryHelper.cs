using TestDemo.WholesaleAssumption;
using TestDemo.Wholesale;
using TestDemo.EclShared;
using System;
using System.Linq;
using Abp.Organizations;
using TestDemo.Authorization.Roles;
using TestDemo.MultiTenancy;

namespace TestDemo.EntityHistory
{
    public static class EntityHistoryHelper
    {
        public const string EntityHistoryConfigurationName = "EntityHistory";

        public static readonly Type[] HostSideTrackedTypes =
        {
            typeof(WholesaleEclApproval),
            typeof(WholesaleEclPdSnPCummulativeDefaultRates),
            typeof(WholesaleEclPdAssumption12Months),
            typeof(WholesaleEclLgdAssumption),
            typeof(WholesaleEclEadInputAssumption),
            typeof(WholesaleEclAssumption),
            typeof(WholesaleEcl),
            typeof(EadInputAssumption),
            typeof(OrganizationUnit), typeof(Role), typeof(Tenant)
        };

        public static readonly Type[] TenantSideTrackedTypes =
        {
            typeof(WholesaleEclApproval),
            typeof(WholesaleEclPdSnPCummulativeDefaultRates),
            typeof(WholesaleEclPdAssumption12Months),
            typeof(WholesaleEclLgdAssumption),
            typeof(WholesaleEclEadInputAssumption),
            typeof(WholesaleEclAssumption),
            typeof(WholesaleEcl),
            typeof(EadInputAssumption),
            typeof(OrganizationUnit), typeof(Role)
        };

        public static readonly Type[] TrackedTypes =
            HostSideTrackedTypes
                .Concat(TenantSideTrackedTypes)
                .GroupBy(type => type.FullName)
                .Select(types => types.First())
                .ToArray();
    }
}
