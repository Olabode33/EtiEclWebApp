using TestDemo.InvestmentInputs.Dtos;
using TestDemo.InvestmentInputs;
using TestDemo.InvestmentAssumption.Dtos;
using TestDemo.InvestmentAssumption;
using TestDemo.Investment.Dtos;
using TestDemo.Investment;
using TestDemo.EclConfig.Dtos;
using TestDemo.EclConfig;

using TestDemo.ObeResults.Dtos;
using TestDemo.ObeResults;
using TestDemo.ObeComputation.Dtos;
using TestDemo.ObeComputation;
using TestDemo.ObeInputs.Dtos;
using TestDemo.ObeInputs;
using TestDemo.ObeAssumption.Dtos;
using TestDemo.ObeAssumption;
using TestDemo.OBE.Dtos;
using TestDemo.OBE;
using TestDemo.RetailResults.Dtos;
using TestDemo.RetailResults;
using TestDemo.RetailComputation.Dtos;
using TestDemo.RetailComputation;
using TestDemo.RetailInputs.Dtos;
using TestDemo.RetailInputs;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.RetailAssumption;
using TestDemo.Retail.Dtos;
using TestDemo.Retail;
using TestDemo.WholesaleResult;
using TestDemo.WholesaleResults.Dtos;
using TestDemo.WholesaleResults;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.WholesaleComputation;
using TestDemo.WholesaleInputs.Dtos;
using TestDemo.WholesaleInputs;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.WholesaleAssumption;
using TestDemo.Wholesale.Dtos;
using TestDemo.Wholesale;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using AutoMapper;
using TestDemo.Auditing.Dto;
using TestDemo.Authorization.Accounts.Dto;
using TestDemo.Authorization.Permissions.Dto;
using TestDemo.Authorization.Roles;
using TestDemo.Authorization.Roles.Dto;
using TestDemo.Authorization.Users;
using TestDemo.Authorization.Users.Dto;
using TestDemo.Authorization.Users.Importing.Dto;
using TestDemo.Authorization.Users.Profile.Dto;
using TestDemo.Chat;
using TestDemo.Chat.Dto;
using TestDemo.Editions;
using TestDemo.Editions.Dto;
using TestDemo.Friendships;
using TestDemo.Friendships.Cache;
using TestDemo.Friendships.Dto;
using TestDemo.Localization.Dto;
using TestDemo.MultiTenancy;
using TestDemo.MultiTenancy.Dto;
using TestDemo.MultiTenancy.HostDashboard.Dto;
using TestDemo.MultiTenancy.Payments;
using TestDemo.MultiTenancy.Payments.Dto;
using TestDemo.Notifications.Dto;
using TestDemo.Organizations.Dto;
using TestDemo.Sessions.Dto;

namespace TestDemo
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateOrEditInvestmentAssetBookDto, InvestmentAssetBook>().ReverseMap();
            configuration.CreateMap<InvestmentAssetBookDto, InvestmentAssetBook>().ReverseMap();
            configuration.CreateMap<CreateOrEditInvestmentEclUploadDto, InvestmentEclUpload>()
                .ForMember(e => e.InvestmentEclId, options => options.MapFrom(dto => dto.EclId))
                .ReverseMap()
                .ForMember(dto => dto.EclId, optopns => optopns.MapFrom(e => e.InvestmentEclId));
            configuration.CreateMap<InvestmentEclUploadDto, InvestmentEclUpload>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclOverrideDto, ObeEclOverride>().ReverseMap();
            configuration.CreateMap<ObeEclOverrideDto, ObeEclOverride>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclOverrideDto, WholesaleEclOverride>().ReverseMap();
            configuration.CreateMap<WholesaleEclOverrideDto, WholesaleEclOverride>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclOverrideDto, RetailEclOverride>().ReverseMap();
            configuration.CreateMap<RetailEclOverrideDto, RetailEclOverride>().ReverseMap();
            configuration.CreateMap<CreateOrEditInvestmentPdInputMacroEconomicAssumptionDto, InvestmentPdInputMacroEconomicAssumption>().ReverseMap();
            configuration.CreateMap<InvestmentPdInputMacroEconomicAssumptionDto, InvestmentPdInputMacroEconomicAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditInvestmentEclPdFitchDefaultRateDto, InvestmentEclPdFitchDefaultRate>().ReverseMap();
            configuration.CreateMap<InvestmentEclPdFitchDefaultRateDto, InvestmentEclPdFitchDefaultRate>().ReverseMap();
            configuration.CreateMap<CreateOrEditInvestmentEclPdInputAssumptionDto, InvestmentEclPdInputAssumption>().ReverseMap();
            configuration.CreateMap<InvestmentEclPdInputAssumptionDto, InvestmentEclPdInputAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditInvestmentEclLgdInputAssumptionDto, InvestmentEclLgdInputAssumption>().ReverseMap();
            configuration.CreateMap<InvestmentEclLgdInputAssumptionDto, InvestmentEclLgdInputAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditInvestmentEclEadInputAssumptionDto, InvestmentEclEadInputAssumption>().ReverseMap();
            configuration.CreateMap<InvestmentEclEadInputAssumptionDto, InvestmentEclEadInputAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditInvSecFitchCummulativeDefaultRateDto, InvSecFitchCummulativeDefaultRate>().ReverseMap();
            configuration.CreateMap<InvSecFitchCummulativeDefaultRateDto, InvSecFitchCummulativeDefaultRate>().ReverseMap();
            configuration.CreateMap<CreateOrEditInvSecMacroEconomicAssumptionDto, InvSecMacroEconomicAssumption>().ReverseMap();
            configuration.CreateMap<InvSecMacroEconomicAssumptionDto, InvSecMacroEconomicAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditInvestmentEclApprovalDto, InvestmentEclApproval>().ReverseMap();
            configuration.CreateMap<InvestmentEclApprovalDto, InvestmentEclApproval>().ReverseMap();
            configuration.CreateMap<CreateOrEditInvestmentEclDto, InvestmentEcl>().ReverseMap();
            configuration.CreateMap<InvestmentEclDto, InvestmentEcl>().ReverseMap();
            configuration.CreateMap<CreateOrEditEclConfigurationDto, EclConfiguration>().ReverseMap();
            configuration.CreateMap<EclConfigurationDto, EclConfiguration>().ReverseMap();
            configuration.CreateMap<CreateOrEditAffiliateOverrideThresholdDto, AffiliateOverrideThreshold>().ReverseMap();
            configuration.CreateMap<AffiliateOverrideThresholdDto, AffiliateOverrideThreshold>().ReverseMap();
            configuration.CreateMap<CreateOrEditAssumptionApprovalDto, AssumptionApproval>().ReverseMap();
            configuration.CreateMap<AssumptionApprovalDto, AssumptionApproval>().ReverseMap();
            configuration.CreateMap<CreateOrEditMacroeconomicVariableDto, MacroeconomicVariable>().ReverseMap();
            configuration.CreateMap<MacroeconomicVariableDto, MacroeconomicVariable>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclPdAssumptionNonInternalModelDto, ObeEclPdAssumptionNonInternalModel>().ReverseMap();
            configuration.CreateMap<ObeEclPdAssumptionNonInternalModelDto, ObeEclPdAssumptionNonInternalModel>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclPdAssumptionNonInteralModelDto, RetailEclPdAssumptionNonInteralModel>().ReverseMap();
            configuration.CreateMap<RetailEclPdAssumptionNonInteralModelDto, RetailEclPdAssumptionNonInteralModel>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclPdAssumptionNplIndexDto, ObeEclPdAssumptionNplIndex>().ReverseMap();
            configuration.CreateMap<ObeEclPdAssumptionNplIndexDto, ObeEclPdAssumptionNplIndex>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclPdAssumptionNplIndexDto, RetailEclPdAssumptionNplIndex>().ReverseMap();
            configuration.CreateMap<RetailEclPdAssumptionNplIndexDto, RetailEclPdAssumptionNplIndex>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclPdAssumptionMacroeconomicProjectionDto, ObeEclPdAssumptionMacroeconomicProjection>().ReverseMap();
            configuration.CreateMap<ObeEclPdAssumptionMacroeconomicProjectionDto, ObeEclPdAssumptionMacroeconomicProjection>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclPdAssumptionMacroeconomicProjectionDto, RetailEclPdAssumptionMacroeconomicProjection>().ReverseMap();
            configuration.CreateMap<RetailEclPdAssumptionMacroeconomicProjectionDto, RetailEclPdAssumptionMacroeconomicProjection>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclPdAssumptionMacroeconomicInputsDto, ObeEclPdAssumptionMacroeconomicInputs>().ReverseMap();
            configuration.CreateMap<ObeEclPdAssumptionMacroeconomicInputsDto, ObeEclPdAssumptionMacroeconomicInputs>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclPdAssumptionMacroeconomicInputDto, RetailEclPdAssumptionMacroeconomicInput>().ReverseMap();
            configuration.CreateMap<RetailEclPdAssumptionMacroeconomicInputDto, RetailEclPdAssumptionMacroeconomicInput>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclPdAssumptionDto, ObeEclPdAssumption>().ReverseMap();
            configuration.CreateMap<ObeEclPdAssumptionDto, ObeEclPdAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclPdAssumptionDto, RetailEclPdAssumption>().ReverseMap();
            configuration.CreateMap<RetailEclPdAssumptionDto, RetailEclPdAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclPdAssumptionNplIndexDto, WholesaleEclPdAssumptionNplIndex>().ReverseMap();
            configuration.CreateMap<WholesaleEclPdAssumptionNplIndexDto, WholesaleEclPdAssumptionNplIndex>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesalePdAssumptionNonInternalModelDto, WholesaleEclPdAssumptionNonInternalModel>().ReverseMap();
            configuration.CreateMap<WholesalePdAssumptionNonInternalModelDto, WholesaleEclPdAssumptionNonInternalModel>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclPdAssumptionMacroeconomicProjectionDto, WholesaleEclPdAssumptionMacroeconomicProjection>().ReverseMap();
            configuration.CreateMap<WholesaleEclPdAssumptionMacroeconomicProjectionDto, WholesaleEclPdAssumptionMacroeconomicProjection>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclPdAssumptionMacroeconomicInputDto, WholesaleEclPdAssumptionMacroeconomicInput>().ReverseMap();
            configuration.CreateMap<WholesaleEclPdAssumptionMacroeconomicInputDto, WholesaleEclPdAssumptionMacroeconomicInput>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclPdAssumptionDto, WholesaleEclPdAssumption>().ReverseMap();
            configuration.CreateMap<WholesaleEclPdAssumptionDto, WholesaleEclPdAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditPdInputAssumptionMacroeconomicProjectionDto, PdInputAssumptionMacroeconomicProjection>().ReverseMap();
            configuration.CreateMap<PdInputAssumptionMacroeconomicProjectionDto, PdInputAssumptionMacroeconomicProjection>().ReverseMap();
            configuration.CreateMap<CreateOrEditPdInputAssumptionNplIndexDto, PdInputAssumptionNplIndex>().ReverseMap();
            configuration.CreateMap<PdInputAssumptionNplIndexDto, PdInputAssumptionNplIndex>().ReverseMap();
            configuration.CreateMap<CreateOrEditPdInputAssumptionStatisticalDto, PdInputAssumptionMacroeconomicInput>().ReverseMap();
            configuration.CreateMap<PdInputAssumptionStatisticalDto, PdInputAssumptionMacroeconomicInput>().ReverseMap();
            configuration.CreateMap<CreateOrEditPdInputAssumptionNonInternalModelDto, PdInputAssumptionNonInternalModel>().ReverseMap();
            configuration.CreateMap<PdInputAssumptionNonInternalModelDto, PdInputAssumptionNonInternalModel>().ReverseMap();
            configuration.CreateMap<CreateOrEditPdInputAssumptionDto, PdInputAssumption>().ReverseMap();
            configuration.CreateMap<PdInputAssumptionDto, PdInputAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclResultSummaryTopExposureDto, ObeEclResultSummaryTopExposure>().ReverseMap();
            configuration.CreateMap<ObeEclResultSummaryTopExposureDto, ObeEclResultSummaryTopExposure>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclResultSummaryKeyInputDto, ObeEclResultSummaryKeyInput>().ReverseMap();
            configuration.CreateMap<ObeEclResultSummaryKeyInputDto, ObeEclResultSummaryKeyInput>().ReverseMap();
            configuration.CreateMap<CreateOrEditObesaleEclResultSummaryDto, ObesaleEclResultSummary>().ReverseMap();
            configuration.CreateMap<ObesaleEclResultSummaryDto, ObesaleEclResultSummary>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclResultDetailDto, ObeEclResultDetail>().ReverseMap();
            configuration.CreateMap<ObeEclResultDetailDto, ObeEclResultDetail>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclSicrApprovalDto, ObeEclSicrApproval>().ReverseMap();
            configuration.CreateMap<ObeEclSicrApprovalDto, ObeEclSicrApproval>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclSicrDto, ObeEclSicr>().ReverseMap();
            configuration.CreateMap<ObeEclSicrDto, ObeEclSicr>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclDataPaymentScheduleDto, ObeEclDataPaymentSchedule>().ReverseMap();
            configuration.CreateMap<ObeEclDataPaymentScheduleDto, ObeEclDataPaymentSchedule>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclDataLoanBookDto, ObeEclDataLoanBook>().ReverseMap();
            configuration.CreateMap<ObeEclDataLoanBookDto, ObeEclDataLoanBook>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclUploadApprovalDto, ObeEclUploadApproval>().ReverseMap();
            configuration.CreateMap<ObeEclUploadApprovalDto, ObeEclUploadApproval>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclUploadDto, ObeEclUpload>().ReverseMap();
            configuration.CreateMap<ObeEclUploadDto, ObeEclUpload>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclPdSnPCummulativeDefaultRateDto, ObeEclPdSnPCummulativeDefaultRate>().ReverseMap();
            configuration.CreateMap<ObeEclPdSnPCummulativeDefaultRateDto, ObeEclPdSnPCummulativeDefaultRate>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclPdAssumption12MonthDto, ObeEclPdAssumption12Month>().ReverseMap();
            configuration.CreateMap<ObeEclPdAssumption12MonthDto, ObeEclPdAssumption12Month>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclLgdAssumptionDto, ObeEclLgdAssumption>().ReverseMap();
            configuration.CreateMap<ObeEclLgdAssumptionDto, ObeEclLgdAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclEadInputAssumptionDto, ObeEclEadInputAssumption>().ReverseMap();
            configuration.CreateMap<ObeEclEadInputAssumptionDto, ObeEclEadInputAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclAssumptionApprovalDto, ObeEclAssumptionApproval>().ReverseMap();
            configuration.CreateMap<ObeEclAssumptionApprovalDto, ObeEclAssumptionApproval>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclAssumptionDto, ObeEclAssumption>().ReverseMap();
            configuration.CreateMap<ObeEclAssumptionDto, ObeEclAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclApprovalDto, ObeEclApproval>().ReverseMap();
            configuration.CreateMap<ObeEclApprovalDto, ObeEclApproval>().ReverseMap();
            configuration.CreateMap<CreateOrEditObeEclDto, ObeEcl>().ReverseMap();
            configuration.CreateMap<ObeEclDto, ObeEcl>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclResultSummaryTopExposureDto, RetailEclResultSummaryTopExposure>().ReverseMap();
            configuration.CreateMap<RetailEclResultSummaryTopExposureDto, RetailEclResultSummaryTopExposure>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclResultSummaryKeyInputDto, RetailEclResultSummaryKeyInput>().ReverseMap();
            configuration.CreateMap<RetailEclResultSummaryKeyInputDto, RetailEclResultSummaryKeyInput>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclResultSummaryDto, RetailEclResultSummary>().ReverseMap();
            configuration.CreateMap<RetailEclResultSummaryDto, RetailEclResultSummary>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclResultDetailDto, RetailEclResultDetail>().ReverseMap();
            configuration.CreateMap<RetailEclResultDetailDto, RetailEclResultDetail>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclSicrApprovalDto, RetailEclSicrApproval>().ReverseMap();
            configuration.CreateMap<RetailEclSicrApprovalDto, RetailEclSicrApproval>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclSicrDto, RetailEclSicr>().ReverseMap();
            configuration.CreateMap<RetailEclSicrDto, RetailEclSicr>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclDataPaymentScheduleDto, RetailEclDataPaymentSchedule>().ReverseMap();
            configuration.CreateMap<RetailEclDataPaymentScheduleDto, RetailEclDataPaymentSchedule>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclDataLoanBookDto, RetailEclDataLoanBook>().ReverseMap();
            configuration.CreateMap<RetailEclDataLoanBookDto, RetailEclDataLoanBook>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclUploadApprovalDto, RetailEclUploadApproval>().ReverseMap();
            configuration.CreateMap<RetailEclUploadApprovalDto, RetailEclUploadApproval>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclUploadDto, RetailEclUpload>().ReverseMap();
            configuration.CreateMap<RetailEclUploadDto, RetailEclUpload>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclPdSnPCummulativeDefaultRateDto, RetailEclPdSnPCummulativeDefaultRate>().ReverseMap();
            configuration.CreateMap<RetailEclPdSnPCummulativeDefaultRateDto, RetailEclPdSnPCummulativeDefaultRate>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclPdAssumption12MonthDto, RetailEclPdAssumption12Month>().ReverseMap();
            configuration.CreateMap<RetailEclPdAssumption12MonthDto, RetailEclPdAssumption12Month>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclLgdAssumptionDto, RetailEclLgdAssumption>().ReverseMap();
            configuration.CreateMap<RetailEclLgdAssumptionDto, RetailEclLgdAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclEadInputAssumptionDto, RetailEclEadInputAssumption>().ReverseMap();
            configuration.CreateMap<RetailEclEadInputAssumptionDto, RetailEclEadInputAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclAssumptionApprovalsDto, RetailEclAssumptionApproval>().ReverseMap();
            configuration.CreateMap<RetailEclAssumptionApprovalsDto, RetailEclAssumptionApproval>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclAssumptionDto, RetailEclAssumption>().ReverseMap();
            configuration.CreateMap<RetailEclAssumptionDto, RetailEclAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclApprovalDto, RetailEclApproval>().ReverseMap();
            configuration.CreateMap<RetailEclApprovalDto, RetailEclApproval>().ReverseMap();
            configuration.CreateMap<CreateOrEditRetailEclDto, RetailEcl>().ReverseMap();
            configuration.CreateMap<RetailEclDto, RetailEcl>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclResultSummaryTopExposureDto, WholesaleEclResultSummaryTopExposure>().ReverseMap();
            configuration.CreateMap<WholesaleEclResultSummaryTopExposureDto, WholesaleEclResultSummaryTopExposure>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclResultSummaryKeyInputDto, WholesaleEclResultSummaryKeyInput>().ReverseMap();
            configuration.CreateMap<WholesaleEclResultSummaryKeyInputDto, WholesaleEclResultSummaryKeyInput>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclResultSummaryDto, WholesaleEclResultSummary>().ReverseMap();
            configuration.CreateMap<WholesaleEclResultSummaryDto, WholesaleEclResultSummary>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclResultDetailDto, WholesaleEclResultDetail>().ReverseMap();
            configuration.CreateMap<WholesaleEclResultDetailDto, WholesaleEclResultDetail>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclSicrApprovalDto, WholesaleEclSicrApproval>().ReverseMap();
            configuration.CreateMap<WholesaleEclSicrApprovalDto, WholesaleEclSicrApproval>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclSicrDto, WholesaleEclSicr>().ReverseMap();
            configuration.CreateMap<WholesaleEclSicrDto, WholesaleEclSicr>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclDataPaymentScheduleDto, WholesaleEclDataPaymentSchedule>().ReverseMap();
            configuration.CreateMap<WholesaleEclDataPaymentScheduleDto, WholesaleEclDataPaymentSchedule>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclDataLoanBookDto, WholesaleEclDataLoanBook>().ReverseMap();
            configuration.CreateMap<WholesaleEclDataLoanBookDto, WholesaleEclDataLoanBook>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclUploadApprovalDto, WholesaleEclUploadApproval>().ReverseMap();
            configuration.CreateMap<WholesaleEclUploadApprovalDto, WholesaleEclUploadApproval>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclUploadDto, WholesaleEclUpload>().ReverseMap();
            configuration.CreateMap<WholesaleEclUploadDto, WholesaleEclUpload>().ReverseMap();
            configuration.CreateMap<CreateOrEditLgdAssumptionUnsecuredRecoveryDto, LgdInputAssumption>().ReverseMap();
            configuration.CreateMap<LgdAssumptionDto, LgdInputAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEadInputAssumptionDto, WholesaleEclEadInputAssumption>().ReverseMap();
            configuration.CreateMap<WholesaleEadInputAssumptionDto, WholesaleEclEadInputAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclApprovalDto, WholesaleEclApproval>().ReverseMap();
            configuration.CreateMap<WholesaleEclApprovalDto, WholesaleEclApproval>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclAssumptionApprovalDto, WholesaleEclAssumptionApproval>().ReverseMap();
            configuration.CreateMap<WholesaleEclAssumptionApprovalDto, WholesaleEclAssumptionApproval>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesDto, WholesaleEclPdSnPCummulativeDefaultRate>().ReverseMap();
            configuration.CreateMap<WholesaleEclPdSnPCummulativeDefaultRatesDto, WholesaleEclPdSnPCummulativeDefaultRate>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclPdAssumption12MonthsDto, WholesaleEclPdAssumption12Month>().ReverseMap();
            configuration.CreateMap<WholesaleEclPdAssumption12MonthsDto, WholesaleEclPdAssumption12Month>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclLgdAssumptionDto, WholesaleEclLgdAssumption>().ReverseMap();
            configuration.CreateMap<WholesaleEclLgdAssumptionDto, WholesaleEclLgdAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEadInputAssumptionDto, WholesaleEclEadInputAssumption>().ReverseMap();
            configuration.CreateMap<WholesaleEadInputAssumptionDto, WholesaleEclEadInputAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclAssumptionDto, WholesaleEclAssumption>().ReverseMap();
            configuration.CreateMap<WholesaleEclAssumptionDto, WholesaleEclAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditWholesaleEclDto, WholesaleEcl>().ReverseMap();
            configuration.CreateMap<WholesaleEclDto, WholesaleEcl>().ReverseMap();
            configuration.CreateMap<CreateOrEditAssumptionDto, Assumption>().ReverseMap();
            configuration.CreateMap<AssumptionDto, Assumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditPdInputSnPCummulativeDefaultRateDto, PdInputAssumptionSnPCummulativeDefaultRate>().ReverseMap();
            configuration.CreateMap<PdInputSnPCummulativeDefaultRateDto, PdInputAssumptionSnPCummulativeDefaultRate>().ReverseMap();
            configuration.CreateMap<CreateOrEditLgdAssumptionUnsecuredRecoveryDto, LgdInputAssumption>().ReverseMap();
            configuration.CreateMap<LgdAssumptionDto, LgdInputAssumption>().ReverseMap();
            configuration.CreateMap<CreateOrEditEadInputAssumptionDto, EadInputAssumption>().ReverseMap();
            configuration.CreateMap<EadInputAssumptionDto, EadInputAssumption>().ReverseMap();
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();


            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
            configuration.CreateMap<CreateOrEditAffiliateAssumptionsDto, AffiliateAssumption>().ReverseMap();
        }
    }
}