using TestDemo.WholesaleInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.WholesaleInputs.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.WholesaleInputs
{
	[AbpAuthorize(AppPermissions.Pages_WholesaleEclDataLoanBooks)]
    public class WholesaleEclDataLoanBooksAppService : TestDemoAppServiceBase, IWholesaleEclDataLoanBooksAppService
    {
		 private readonly IRepository<WholesaleEclDataLoanBook, Guid> _wholesaleEclDataLoanBookRepository;
		 private readonly IRepository<WholesaleEclUpload,Guid> _lookup_wholesaleEclUploadRepository;
		 

		  public WholesaleEclDataLoanBooksAppService(IRepository<WholesaleEclDataLoanBook, Guid> wholesaleEclDataLoanBookRepository , IRepository<WholesaleEclUpload, Guid> lookup_wholesaleEclUploadRepository) 
		  {
			_wholesaleEclDataLoanBookRepository = wholesaleEclDataLoanBookRepository;
			_lookup_wholesaleEclUploadRepository = lookup_wholesaleEclUploadRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclDataLoanBookForViewDto>> GetAll(GetAllWholesaleEclDataLoanBooksInput input)
         {
			
			var filteredWholesaleEclDataLoanBooks = _wholesaleEclDataLoanBookRepository.GetAll()
						.Include( e => e.WholesaleEclUploadFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.CustomerNo.Contains(input.Filter) || e.AccountNo.Contains(input.Filter) || e.ContractNo.Contains(input.Filter) || e.CustomerName.Contains(input.Filter) || e.Segment.Contains(input.Filter) || e.Sector.Contains(input.Filter) || e.Currency.Contains(input.Filter) || e.ProductType.Contains(input.Filter) || e.ProductMapping.Contains(input.Filter) || e.SpecialisedLending.Contains(input.Filter) || e.RatingModel.Contains(input.Filter) || e.Classification.Contains(input.Filter) || e.RestructureRisk.Contains(input.Filter) || e.RestructureType.Contains(input.Filter) || e.PrincipalPaymentTermsOrigination.Contains(input.Filter) || e.InterestPaymentTermsOrigination.Contains(input.Filter) || e.PrincipalPaymentStructure.Contains(input.Filter) || e.InterestPaymentStructure.Contains(input.Filter) || e.InterestRateType.Contains(input.Filter) || e.BaseRate.Contains(input.Filter) || e.OriginationContractualInterestRate.Contains(input.Filter) || e.GuarantorPD.Contains(input.Filter) || e.GuarantorLGD.Contains(input.Filter) || e.ContractId.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CustomerNoFilter),  e => e.CustomerNo.ToLower() == input.CustomerNoFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.AccountNoFilter),  e => e.AccountNo.ToLower() == input.AccountNoFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContractNoFilter),  e => e.ContractNo.ToLower() == input.ContractNoFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CustomerNameFilter),  e => e.CustomerName.ToLower() == input.CustomerNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CurrencyFilter),  e => e.Currency.ToLower() == input.CurrencyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductTypeFilter),  e => e.ProductType.ToLower() == input.ProductTypeFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductMappingFilter),  e => e.ProductMapping.ToLower() == input.ProductMappingFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.SpecialisedLendingFilter),  e => e.SpecialisedLending.ToLower() == input.SpecialisedLendingFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.RatingModelFilter),  e => e.RatingModel.ToLower() == input.RatingModelFilter.ToLower().Trim())
						.WhereIf(input.MinOriginalRatingFilter != null, e => e.OriginalRating >= input.MinOriginalRatingFilter)
						.WhereIf(input.MaxOriginalRatingFilter != null, e => e.OriginalRating <= input.MaxOriginalRatingFilter)
						.WhereIf(input.MinCurrentRatingFilter != null, e => e.CurrentRating >= input.MinCurrentRatingFilter)
						.WhereIf(input.MaxCurrentRatingFilter != null, e => e.CurrentRating <= input.MaxCurrentRatingFilter)
						.WhereIf(input.MinLifetimePDFilter != null, e => e.LifetimePD >= input.MinLifetimePDFilter)
						.WhereIf(input.MaxLifetimePDFilter != null, e => e.LifetimePD <= input.MaxLifetimePDFilter)
						.WhereIf(input.MinMonth12PDFilter != null, e => e.Month12PD >= input.MinMonth12PDFilter)
						.WhereIf(input.MaxMonth12PDFilter != null, e => e.Month12PD <= input.MaxMonth12PDFilter)
						.WhereIf(input.MinDaysPastDueFilter != null, e => e.DaysPastDue >= input.MinDaysPastDueFilter)
						.WhereIf(input.MaxDaysPastDueFilter != null, e => e.DaysPastDue <= input.MaxDaysPastDueFilter)
						.WhereIf(input.WatchlistIndicatorFilter > -1,  e => Convert.ToInt32(e.WatchlistIndicator) == input.WatchlistIndicatorFilter )
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClassificationFilter),  e => e.Classification.ToLower() == input.ClassificationFilter.ToLower().Trim())
						.WhereIf(input.MinImpairedDateFilter != null, e => e.ImpairedDate >= input.MinImpairedDateFilter)
						.WhereIf(input.MaxImpairedDateFilter != null, e => e.ImpairedDate <= input.MaxImpairedDateFilter)
						.WhereIf(input.MinDefaultDateFilter != null, e => e.DefaultDate >= input.MinDefaultDateFilter)
						.WhereIf(input.MaxDefaultDateFilter != null, e => e.DefaultDate <= input.MaxDefaultDateFilter)
						.WhereIf(input.MinCreditLimitFilter != null, e => e.CreditLimit >= input.MinCreditLimitFilter)
						.WhereIf(input.MaxCreditLimitFilter != null, e => e.CreditLimit <= input.MaxCreditLimitFilter)
						.WhereIf(input.MinOriginalBalanceLCYFilter != null, e => e.OriginalBalanceLCY >= input.MinOriginalBalanceLCYFilter)
						.WhereIf(input.MaxOriginalBalanceLCYFilter != null, e => e.OriginalBalanceLCY <= input.MaxOriginalBalanceLCYFilter)
						.WhereIf(input.MinOutstandingBalanceLCYFilter != null, e => e.OutstandingBalanceLCY >= input.MinOutstandingBalanceLCYFilter)
						.WhereIf(input.MaxOutstandingBalanceLCYFilter != null, e => e.OutstandingBalanceLCY <= input.MaxOutstandingBalanceLCYFilter)
						.WhereIf(input.MinOutstandingBalanceACYFilter != null, e => e.OutstandingBalanceACY >= input.MinOutstandingBalanceACYFilter)
						.WhereIf(input.MaxOutstandingBalanceACYFilter != null, e => e.OutstandingBalanceACY <= input.MaxOutstandingBalanceACYFilter)
						.WhereIf(input.MinContractStartDateFilter != null, e => e.ContractStartDate >= input.MinContractStartDateFilter)
						.WhereIf(input.MaxContractStartDateFilter != null, e => e.ContractStartDate <= input.MaxContractStartDateFilter)
						.WhereIf(input.MinContractEndDateFilter != null, e => e.ContractEndDate >= input.MinContractEndDateFilter)
						.WhereIf(input.MaxContractEndDateFilter != null, e => e.ContractEndDate <= input.MaxContractEndDateFilter)
						.WhereIf(input.RestructureIndicatorFilter > -1,  e => Convert.ToInt32(e.RestructureIndicator) == input.RestructureIndicatorFilter )
						.WhereIf(!string.IsNullOrWhiteSpace(input.RestructureRiskFilter),  e => e.RestructureRisk.ToLower() == input.RestructureRiskFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.RestructureTypeFilter),  e => e.RestructureType.ToLower() == input.RestructureTypeFilter.ToLower().Trim())
						.WhereIf(input.MinRestructureStartDateFilter != null, e => e.RestructureStartDate >= input.MinRestructureStartDateFilter)
						.WhereIf(input.MaxRestructureStartDateFilter != null, e => e.RestructureStartDate <= input.MaxRestructureStartDateFilter)
						.WhereIf(input.MinRestructureEndDateFilter != null, e => e.RestructureEndDate >= input.MinRestructureEndDateFilter)
						.WhereIf(input.MaxRestructureEndDateFilter != null, e => e.RestructureEndDate <= input.MaxRestructureEndDateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PrincipalPaymentTermsOriginationFilter),  e => e.PrincipalPaymentTermsOrigination.ToLower() == input.PrincipalPaymentTermsOriginationFilter.ToLower().Trim())
						.WhereIf(input.MinPPTOPeriodFilter != null, e => e.PPTOPeriod >= input.MinPPTOPeriodFilter)
						.WhereIf(input.MaxPPTOPeriodFilter != null, e => e.PPTOPeriod <= input.MaxPPTOPeriodFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.InterestPaymentTermsOriginationFilter),  e => e.InterestPaymentTermsOrigination.ToLower() == input.InterestPaymentTermsOriginationFilter.ToLower().Trim())
						.WhereIf(input.MinIPTOPeriodFilter != null, e => e.IPTOPeriod >= input.MinIPTOPeriodFilter)
						.WhereIf(input.MaxIPTOPeriodFilter != null, e => e.IPTOPeriod <= input.MaxIPTOPeriodFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PrincipalPaymentStructureFilter),  e => e.PrincipalPaymentStructure.ToLower() == input.PrincipalPaymentStructureFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.InterestPaymentStructureFilter),  e => e.InterestPaymentStructure.ToLower() == input.InterestPaymentStructureFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.InterestRateTypeFilter),  e => e.InterestRateType.ToLower() == input.InterestRateTypeFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.BaseRateFilter),  e => e.BaseRate.ToLower() == input.BaseRateFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.OriginationContractualInterestRateFilter),  e => e.OriginationContractualInterestRate.ToLower() == input.OriginationContractualInterestRateFilter.ToLower().Trim())
						.WhereIf(input.MinIntroductoryPeriodFilter != null, e => e.IntroductoryPeriod >= input.MinIntroductoryPeriodFilter)
						.WhereIf(input.MaxIntroductoryPeriodFilter != null, e => e.IntroductoryPeriod <= input.MaxIntroductoryPeriodFilter)
						.WhereIf(input.MinPostIPContractualInterestRateFilter != null, e => e.PostIPContractualInterestRate >= input.MinPostIPContractualInterestRateFilter)
						.WhereIf(input.MaxPostIPContractualInterestRateFilter != null, e => e.PostIPContractualInterestRate <= input.MaxPostIPContractualInterestRateFilter)
						.WhereIf(input.MinCurrentContractualInterestRateFilter != null, e => e.CurrentContractualInterestRate >= input.MinCurrentContractualInterestRateFilter)
						.WhereIf(input.MaxCurrentContractualInterestRateFilter != null, e => e.CurrentContractualInterestRate <= input.MaxCurrentContractualInterestRateFilter)
						.WhereIf(input.MinEIRFilter != null, e => e.EIR >= input.MinEIRFilter)
						.WhereIf(input.MaxEIRFilter != null, e => e.EIR <= input.MaxEIRFilter)
						.WhereIf(input.MinDebentureOMVFilter != null, e => e.DebentureOMV >= input.MinDebentureOMVFilter)
						.WhereIf(input.MaxDebentureOMVFilter != null, e => e.DebentureOMV <= input.MaxDebentureOMVFilter)
						.WhereIf(input.MinDebentureFSVFilter != null, e => e.DebentureFSV >= input.MinDebentureFSVFilter)
						.WhereIf(input.MaxDebentureFSVFilter != null, e => e.DebentureFSV <= input.MaxDebentureFSVFilter)
						.WhereIf(input.MinCashOMVFilter != null, e => e.CashOMV >= input.MinCashOMVFilter)
						.WhereIf(input.MaxCashOMVFilter != null, e => e.CashOMV <= input.MaxCashOMVFilter)
						.WhereIf(input.MinCashFSVFilter != null, e => e.CashFSV >= input.MinCashFSVFilter)
						.WhereIf(input.MaxCashFSVFilter != null, e => e.CashFSV <= input.MaxCashFSVFilter)
						.WhereIf(input.MinInventoryOMVFilter != null, e => e.InventoryOMV >= input.MinInventoryOMVFilter)
						.WhereIf(input.MaxInventoryOMVFilter != null, e => e.InventoryOMV <= input.MaxInventoryOMVFilter)
						.WhereIf(input.MinInventoryFSVFilter != null, e => e.InventoryFSV >= input.MinInventoryFSVFilter)
						.WhereIf(input.MaxInventoryFSVFilter != null, e => e.InventoryFSV <= input.MaxInventoryFSVFilter)
						.WhereIf(input.MinPlantEquipmentOMVFilter != null, e => e.PlantEquipmentOMV >= input.MinPlantEquipmentOMVFilter)
						.WhereIf(input.MaxPlantEquipmentOMVFilter != null, e => e.PlantEquipmentOMV <= input.MaxPlantEquipmentOMVFilter)
						.WhereIf(input.MinPlantEquipmentFSVFilter != null, e => e.PlantEquipmentFSV >= input.MinPlantEquipmentFSVFilter)
						.WhereIf(input.MaxPlantEquipmentFSVFilter != null, e => e.PlantEquipmentFSV <= input.MaxPlantEquipmentFSVFilter)
						.WhereIf(input.MinResidentialPropertyOMVFilter != null, e => e.ResidentialPropertyOMV >= input.MinResidentialPropertyOMVFilter)
						.WhereIf(input.MaxResidentialPropertyOMVFilter != null, e => e.ResidentialPropertyOMV <= input.MaxResidentialPropertyOMVFilter)
						.WhereIf(input.MinResidentialPropertyFSVFilter != null, e => e.ResidentialPropertyFSV >= input.MinResidentialPropertyFSVFilter)
						.WhereIf(input.MaxResidentialPropertyFSVFilter != null, e => e.ResidentialPropertyFSV <= input.MaxResidentialPropertyFSVFilter)
						.WhereIf(input.MinCommercialPropertyOMVFilter != null, e => e.CommercialPropertyOMV >= input.MinCommercialPropertyOMVFilter)
						.WhereIf(input.MaxCommercialPropertyOMVFilter != null, e => e.CommercialPropertyOMV <= input.MaxCommercialPropertyOMVFilter)
						.WhereIf(input.MinCommercialPropertyFilter != null, e => e.CommercialProperty >= input.MinCommercialPropertyFilter)
						.WhereIf(input.MaxCommercialPropertyFilter != null, e => e.CommercialProperty <= input.MaxCommercialPropertyFilter)
						.WhereIf(input.MinReceivablesOMVFilter != null, e => e.ReceivablesOMV >= input.MinReceivablesOMVFilter)
						.WhereIf(input.MaxReceivablesOMVFilter != null, e => e.ReceivablesOMV <= input.MaxReceivablesOMVFilter)
						.WhereIf(input.MinReceivablesFSVFilter != null, e => e.ReceivablesFSV >= input.MinReceivablesFSVFilter)
						.WhereIf(input.MaxReceivablesFSVFilter != null, e => e.ReceivablesFSV <= input.MaxReceivablesFSVFilter)
						.WhereIf(input.MinSharesOMVFilter != null, e => e.SharesOMV >= input.MinSharesOMVFilter)
						.WhereIf(input.MaxSharesOMVFilter != null, e => e.SharesOMV <= input.MaxSharesOMVFilter)
						.WhereIf(input.MinSharesFSVFilter != null, e => e.SharesFSV >= input.MinSharesFSVFilter)
						.WhereIf(input.MaxSharesFSVFilter != null, e => e.SharesFSV <= input.MaxSharesFSVFilter)
						.WhereIf(input.MinVehicleOMVFilter != null, e => e.VehicleOMV >= input.MinVehicleOMVFilter)
						.WhereIf(input.MaxVehicleOMVFilter != null, e => e.VehicleOMV <= input.MaxVehicleOMVFilter)
						.WhereIf(input.MinVehicleFSVFilter != null, e => e.VehicleFSV >= input.MinVehicleFSVFilter)
						.WhereIf(input.MaxVehicleFSVFilter != null, e => e.VehicleFSV <= input.MaxVehicleFSVFilter)
						.WhereIf(input.MinCureRateFilter != null, e => e.CureRate >= input.MinCureRateFilter)
						.WhereIf(input.MaxCureRateFilter != null, e => e.CureRate <= input.MaxCureRateFilter)
						.WhereIf(input.GuaranteeIndicatorFilter > -1,  e => Convert.ToInt32(e.GuaranteeIndicator) == input.GuaranteeIndicatorFilter )
						.WhereIf(!string.IsNullOrWhiteSpace(input.GuarantorPDFilter),  e => e.GuarantorPD.ToLower() == input.GuarantorPDFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.GuarantorLGDFilter),  e => e.GuarantorLGD.ToLower() == input.GuarantorLGDFilter.ToLower().Trim())
						.WhereIf(input.MinGuaranteeValueFilter != null, e => e.GuaranteeValue >= input.MinGuaranteeValueFilter)
						.WhereIf(input.MaxGuaranteeValueFilter != null, e => e.GuaranteeValue <= input.MaxGuaranteeValueFilter)
						.WhereIf(input.MinGuaranteeLevelFilter != null, e => e.GuaranteeLevel >= input.MinGuaranteeLevelFilter)
						.WhereIf(input.MaxGuaranteeLevelFilter != null, e => e.GuaranteeLevel <= input.MaxGuaranteeLevelFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContractIdFilter),  e => e.ContractId.ToLower() == input.ContractIdFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.WholesaleEclUploadUploadCommentFilter), e => e.WholesaleEclUploadFk != null && e.WholesaleEclUploadFk.UploadComment.ToLower() == input.WholesaleEclUploadUploadCommentFilter.ToLower().Trim());

			var pagedAndFilteredWholesaleEclDataLoanBooks = filteredWholesaleEclDataLoanBooks
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclDataLoanBooks = from o in pagedAndFilteredWholesaleEclDataLoanBooks
                         join o1 in _lookup_wholesaleEclUploadRepository.GetAll() on o.WholesaleEclUploadId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclDataLoanBookForViewDto() {
							WholesaleEclDataLoanBook = new WholesaleEclDataLoanBookDto
							{
                                CustomerNo = o.CustomerNo,
                                AccountNo = o.AccountNo,
                                ContractNo = o.ContractNo,
                                CustomerName = o.CustomerName,
                                Currency = o.Currency,
                                ProductType = o.ProductType,
                                ProductMapping = o.ProductMapping,
                                SpecialisedLending = o.SpecialisedLending,
                                RatingModel = o.RatingModel,
                                OriginalRating = o.OriginalRating,
                                CurrentRating = o.CurrentRating,
                                LifetimePD = o.LifetimePD,
                                Month12PD = o.Month12PD,
                                DaysPastDue = o.DaysPastDue,
                                WatchlistIndicator = o.WatchlistIndicator,
                                Classification = o.Classification,
                                ImpairedDate = o.ImpairedDate,
                                DefaultDate = o.DefaultDate,
                                CreditLimit = o.CreditLimit,
                                OriginalBalanceLCY = o.OriginalBalanceLCY,
                                OutstandingBalanceLCY = o.OutstandingBalanceLCY,
                                OutstandingBalanceACY = o.OutstandingBalanceACY,
                                ContractStartDate = o.ContractStartDate,
                                ContractEndDate = o.ContractEndDate,
                                RestructureIndicator = o.RestructureIndicator,
                                RestructureRisk = o.RestructureRisk,
                                RestructureType = o.RestructureType,
                                RestructureStartDate = o.RestructureStartDate,
                                RestructureEndDate = o.RestructureEndDate,
                                PrincipalPaymentTermsOrigination = o.PrincipalPaymentTermsOrigination,
                                PPTOPeriod = o.PPTOPeriod,
                                InterestPaymentTermsOrigination = o.InterestPaymentTermsOrigination,
                                IPTOPeriod = o.IPTOPeriod,
                                PrincipalPaymentStructure = o.PrincipalPaymentStructure,
                                InterestPaymentStructure = o.InterestPaymentStructure,
                                InterestRateType = o.InterestRateType,
                                BaseRate = o.BaseRate,
                                OriginationContractualInterestRate = o.OriginationContractualInterestRate,
                                IntroductoryPeriod = o.IntroductoryPeriod,
                                PostIPContractualInterestRate = o.PostIPContractualInterestRate,
                                CurrentContractualInterestRate = o.CurrentContractualInterestRate,
                                EIR = o.EIR,
                                DebentureOMV = o.DebentureOMV,
                                DebentureFSV = o.DebentureFSV,
                                CashOMV = o.CashOMV,
                                CashFSV = o.CashFSV,
                                InventoryOMV = o.InventoryOMV,
                                InventoryFSV = o.InventoryFSV,
                                PlantEquipmentOMV = o.PlantEquipmentOMV,
                                PlantEquipmentFSV = o.PlantEquipmentFSV,
                                ResidentialPropertyOMV = o.ResidentialPropertyOMV,
                                ResidentialPropertyFSV = o.ResidentialPropertyFSV,
                                CommercialPropertyOMV = o.CommercialPropertyOMV,
                                CommercialProperty = o.CommercialProperty,
                                ReceivablesOMV = o.ReceivablesOMV,
                                ReceivablesFSV = o.ReceivablesFSV,
                                SharesOMV = o.SharesOMV,
                                SharesFSV = o.SharesFSV,
                                VehicleOMV = o.VehicleOMV,
                                VehicleFSV = o.VehicleFSV,
                                CureRate = o.CureRate,
                                GuaranteeIndicator = o.GuaranteeIndicator,
                                GuarantorPD = o.GuarantorPD,
                                GuarantorLGD = o.GuarantorLGD,
                                GuaranteeValue = o.GuaranteeValue,
                                GuaranteeLevel = o.GuaranteeLevel,
                                ContractId = o.ContractId,
                                Id = o.Id
							},
                         	WholesaleEclUploadUploadComment = s1 == null ? "" : s1.UploadComment.ToString()
						};

            var totalCount = await filteredWholesaleEclDataLoanBooks.CountAsync();

            return new PagedResultDto<GetWholesaleEclDataLoanBookForViewDto>(
                totalCount,
                await wholesaleEclDataLoanBooks.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclDataLoanBooks_Edit)]
		 public async Task<GetWholesaleEclDataLoanBookForEditOutput> GetWholesaleEclDataLoanBookForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclDataLoanBook = await _wholesaleEclDataLoanBookRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclDataLoanBookForEditOutput {WholesaleEclDataLoanBook = ObjectMapper.Map<CreateOrEditWholesaleEclDataLoanBookDto>(wholesaleEclDataLoanBook)};

		    if (output.WholesaleEclDataLoanBook.WholesaleEclUploadId != null)
            {
                var _lookupWholesaleEclUpload = await _lookup_wholesaleEclUploadRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclDataLoanBook.WholesaleEclUploadId);
                output.WholesaleEclUploadUploadComment = _lookupWholesaleEclUpload.UploadComment.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclDataLoanBookDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclDataLoanBooks_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEclDataLoanBookDto input)
         {
            var wholesaleEclDataLoanBook = ObjectMapper.Map<WholesaleEclDataLoanBook>(input);

			
			if (AbpSession.TenantId != null)
			{
				wholesaleEclDataLoanBook.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _wholesaleEclDataLoanBookRepository.InsertAsync(wholesaleEclDataLoanBook);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclDataLoanBooks_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEclDataLoanBookDto input)
         {
            var wholesaleEclDataLoanBook = await _wholesaleEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclDataLoanBook);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclDataLoanBooks_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclDataLoanBookRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEclDataLoanBooks)]
         public async Task<PagedResultDto<WholesaleEclDataLoanBookWholesaleEclUploadLookupTableDto>> GetAllWholesaleEclUploadForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclUploadRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.UploadComment.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclUploadList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclDataLoanBookWholesaleEclUploadLookupTableDto>();
			foreach(var wholesaleEclUpload in wholesaleEclUploadList){
				lookupTableDtoList.Add(new WholesaleEclDataLoanBookWholesaleEclUploadLookupTableDto
				{
					Id = wholesaleEclUpload.Id.ToString(),
					DisplayName = wholesaleEclUpload.UploadComment?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclDataLoanBookWholesaleEclUploadLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}