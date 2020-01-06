using TestDemo.RetailInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailInputs.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.RetailInputs
{
	[AbpAuthorize(AppPermissions.Pages_RetailEclDataLoanBooks)]
    public class RetailEclDataLoanBooksAppService : TestDemoAppServiceBase, IRetailEclDataLoanBooksAppService
    {
		 private readonly IRepository<RetailEclDataLoanBook, Guid> _retailEclDataLoanBookRepository;
		 private readonly IRepository<RetailEclUpload,Guid> _lookup_retailEclUploadRepository;
		 

		  public RetailEclDataLoanBooksAppService(IRepository<RetailEclDataLoanBook, Guid> retailEclDataLoanBookRepository , IRepository<RetailEclUpload, Guid> lookup_retailEclUploadRepository) 
		  {
			_retailEclDataLoanBookRepository = retailEclDataLoanBookRepository;
			_lookup_retailEclUploadRepository = lookup_retailEclUploadRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclDataLoanBookForViewDto>> GetAll(GetAllRetailEclDataLoanBooksInput input)
         {
			
			var filteredRetailEclDataLoanBooks = _retailEclDataLoanBookRepository.GetAll()
						.Include( e => e.RetailEclUploadFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.CustomerNo.Contains(input.Filter) || e.AccountNo.Contains(input.Filter) || e.ContractNo.Contains(input.Filter) || e.CustomerName.Contains(input.Filter) || e.Segment.Contains(input.Filter) || e.Sector.Contains(input.Filter) || e.Currency.Contains(input.Filter) || e.ProductType.Contains(input.Filter) || e.ProductMapping.Contains(input.Filter) || e.SpecialisedLending.Contains(input.Filter) || e.RatingModel.Contains(input.Filter) || e.Classification.Contains(input.Filter) || e.RestructureRisk.Contains(input.Filter) || e.RestructureType.Contains(input.Filter) || e.PrincipalPaymentTermsOrigination.Contains(input.Filter) || e.InterestPaymentTermsOrigination.Contains(input.Filter) || e.PrincipalPaymentStructure.Contains(input.Filter) || e.InterestPaymentStructure.Contains(input.Filter) || e.InterestRateType.Contains(input.Filter) || e.BaseRate.Contains(input.Filter) || e.OriginationContractualInterestRate.Contains(input.Filter) || e.GuarantorPD.Contains(input.Filter) || e.GuarantorLGD.Contains(input.Filter) || e.ContractId.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CustomerNoFilter),  e => e.CustomerNo.ToLower() == input.CustomerNoFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.AccountNoFilter),  e => e.AccountNo.ToLower() == input.AccountNoFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContractNoFilter),  e => e.ContractNo.ToLower() == input.ContractNoFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CustomerNameFilter),  e => e.CustomerName.ToLower() == input.CustomerNameFilter.ToLower().Trim());

			var pagedAndFilteredRetailEclDataLoanBooks = filteredRetailEclDataLoanBooks
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclDataLoanBooks = from o in pagedAndFilteredRetailEclDataLoanBooks
                         join o1 in _lookup_retailEclUploadRepository.GetAll() on o.RetailEclUploadId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEclDataLoanBookForViewDto() {
							EclDataLoanBook = new RetailEclDataLoanBookDto
							{
                                CustomerNo = o.CustomerNo,
                                AccountNo = o.AccountNo,
                                ContractNo = o.ContractNo,
                                CustomerName = o.CustomerName,
                                SnapshotDate = o.SnapshotDate,
                                Segment = o.Segment,
                                Sector = o.Sector,
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
                         	RetailEclUploadTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailEclDataLoanBooks.CountAsync();

            return new PagedResultDto<GetRetailEclDataLoanBookForViewDto>(
                totalCount,
                await retailEclDataLoanBooks.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclDataLoanBooks_Edit)]
		 public async Task<GetRetailEclDataLoanBookForEditOutput> GetRetailEclDataLoanBookForEdit(EntityDto<Guid> input)
         {
            var retailEclDataLoanBook = await _retailEclDataLoanBookRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclDataLoanBookForEditOutput {RetailEclDataLoanBook = ObjectMapper.Map<CreateOrEditRetailEclDataLoanBookDto>(retailEclDataLoanBook)};

		    if (output.RetailEclDataLoanBook.RetailEclUploadId != null)
            {
                var _lookupRetailEclUpload = await _lookup_retailEclUploadRepository.FirstOrDefaultAsync((Guid)output.RetailEclDataLoanBook.RetailEclUploadId);
                output.RetailEclUploadTenantId = _lookupRetailEclUpload.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclDataLoanBookDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclDataLoanBooks_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclDataLoanBookDto input)
         {
            var retailEclDataLoanBook = ObjectMapper.Map<RetailEclDataLoanBook>(input);

			
			if (AbpSession.TenantId != null)
			{
				retailEclDataLoanBook.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _retailEclDataLoanBookRepository.InsertAsync(retailEclDataLoanBook);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclDataLoanBooks_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclDataLoanBookDto input)
         {
            var retailEclDataLoanBook = await _retailEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclDataLoanBook);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclDataLoanBooks_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclDataLoanBookRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclDataLoanBooks)]
         public async Task<PagedResultDto<RetailEclDataLoanBookRetailEclUploadLookupTableDto>> GetAllRetailEclUploadForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclUploadRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclUploadList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclDataLoanBookRetailEclUploadLookupTableDto>();
			foreach(var retailEclUpload in retailEclUploadList){
				lookupTableDtoList.Add(new RetailEclDataLoanBookRetailEclUploadLookupTableDto
				{
					Id = retailEclUpload.Id.ToString(),
					DisplayName = retailEclUpload.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEclDataLoanBookRetailEclUploadLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}