using TestDemo.InvestmentAssumption;
using TestDemo.Investment;
using TestDemo.EclConfig;
using TestDemo.LgdCalibrationResult;
using TestDemo.LgdCalibrationResult;
using TestDemo.EadCalibrationResult;
using TestDemo.GeneralCalibrationResult;
using TestDemo.PdCalibrationResult;
using TestDemo.ObeComputation;
using TestDemo.ObeInputs;
using TestDemo.ObeAssumption;
using TestDemo.OBE;
using TestDemo.RetailComputation;
using TestDemo.RetailInputs;
using TestDemo.RetailAssumption;
using TestDemo.Retail;
using TestDemo.WholesaleComputation;
using TestDemo.WholesaleInputs;
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
            typeof(ObeEclSicrApproval),
            typeof(ObeEclSicr),
            typeof(ObeEclUploadApproval),
            typeof(ObeEclUpload),
            typeof(ObeEclPdSnPCummulativeDefaultRate),
            typeof(ObeEclPdAssumption12Month),
            typeof(ObeEclLgdAssumption),
            typeof(ObeEclEadInputAssumption),
            typeof(ObeEclAssumptionApproval),
            typeof(ObeEclAssumption),
            typeof(ObeEclApproval),
            typeof(RetailEclSicrApproval),
            typeof(RetailEclSicr),
            typeof(RetailEclUploadApproval),
            typeof(RetailEclUpload),
            typeof(RetailEclPdSnPCummulativeDefaultRate),
            typeof(RetailEclPdAssumption12Month),
            typeof(RetailEclLgdAssumption),
            typeof(RetailEclEadInputAssumption),
            typeof(RetailEclAssumptionApproval),
            typeof(RetailEclAssumption),
            typeof(RetailEclApproval),
            typeof(RetailEcl),
            typeof(WholesaleEclSicrApproval),
            typeof(WholesaleEclSicr),
            typeof(WholesaleEclUploadApproval),
            typeof(WholesaleEclUpload),
            typeof(WholesaleEclEadInputAssumption),
            typeof(WholesaleEclApproval),
            typeof(WholesaleEclPdSnPCummulativeDefaultRate),
            typeof(WholesaleEclPdAssumption12Month),
            typeof(WholesaleEclLgdAssumption),
            typeof(WholesaleEclEadInputAssumption),
            typeof(WholesaleEclAssumption),
            typeof(WholesaleEcl),
            typeof(EadInputAssumption),
            typeof(OrganizationUnit), typeof(Role), typeof(Tenant)
        };

        public static readonly Type[] TenantSideTrackedTypes =
        {
            typeof(ObeEclSicrApproval),
            typeof(ObeEclSicr),
            typeof(ObeEclUploadApproval),
            typeof(ObeEclUpload),
            typeof(ObeEclPdSnPCummulativeDefaultRate),
            typeof(ObeEclPdAssumption12Month),
            typeof(ObeEclLgdAssumption),
            typeof(ObeEclEadInputAssumption),
            typeof(ObeEclAssumptionApproval),
            typeof(ObeEclAssumption),
            typeof(ObeEclApproval),
            typeof(RetailEclSicrApproval),
            typeof(RetailEclSicr),
            typeof(RetailEclUploadApproval),
            typeof(RetailEclUpload),
            typeof(RetailEclPdSnPCummulativeDefaultRate),
            typeof(RetailEclPdAssumption12Month),
            typeof(RetailEclLgdAssumption),
            typeof(RetailEclEadInputAssumption),
            typeof(RetailEclAssumptionApproval),
            typeof(RetailEclAssumption),
            typeof(RetailEclApproval),
            typeof(RetailEcl),
            typeof(WholesaleEclSicrApproval),
            typeof(WholesaleEclSicr),
            typeof(WholesaleEclUploadApproval),
            typeof(WholesaleEclUpload),
            typeof(WholesaleEclEadInputAssumption),
            typeof(WholesaleEclApproval),
            typeof(WholesaleEclPdSnPCummulativeDefaultRate),
            typeof(WholesaleEclPdAssumption12Month),
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
