using TestDemo.ObeInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ObeInputs.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.ObeInputs
{
    public class ObeEclDataLoanBooksAppService : TestDemoAppServiceBase, IObeEclDataLoanBooksAppService
    {
		 private readonly IRepository<ObeEclDataLoanBook, Guid> _obeEclDataLoanBookRepository;
		 private readonly IRepository<ObeEclUpload,Guid> _lookup_obeEclUploadRepository;
		 

		  public ObeEclDataLoanBooksAppService(IRepository<ObeEclDataLoanBook, Guid> obeEclDataLoanBookRepository , IRepository<ObeEclUpload, Guid> lookup_obeEclUploadRepository) 
		  {
			_obeEclDataLoanBookRepository = obeEclDataLoanBookRepository;
			_lookup_obeEclUploadRepository = lookup_obeEclUploadRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclDataLoanBookForViewDto>> GetAll(GetAllObeEclDataLoanBooksInput input)
         {
			
			var filteredObeEclDataLoanBooks = _obeEclDataLoanBookRepository.GetAll()
						//.Include( e => e.ObeEclUploadFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.CustomerNo.Contains(input.Filter) || e.AccountNo.Contains(input.Filter) || e.ContractNo.Contains(input.Filter) || e.CustomerName.Contains(input.Filter) || e.Segment.Contains(input.Filter) || e.Sector.Contains(input.Filter) || e.Currency.Contains(input.Filter) || e.ProductType.Contains(input.Filter) || e.ProductMapping.Contains(input.Filter) || e.SpecialisedLending.Contains(input.Filter) || e.RatingModel.Contains(input.Filter) || e.Classification.Contains(input.Filter) || e.RestructureRisk.Contains(input.Filter) || e.RestructureType.Contains(input.Filter) || e.PrincipalPaymentTermsOrigination.Contains(input.Filter) || e.InterestPaymentTermsOrigination.Contains(input.Filter) || e.PrincipalPaymentStructure.Contains(input.Filter) || e.InterestPaymentStructure.Contains(input.Filter) || e.InterestRateType.Contains(input.Filter) || e.BaseRate.Contains(input.Filter) || e.OriginationContractualInterestRate.Contains(input.Filter) || e.GuarantorPD.Contains(input.Filter) || e.GuarantorLGD.Contains(input.Filter) || e.ContractId.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContractIdFilter),  e => e.ContractId.ToLower() == input.ContractIdFilter.ToLower().Trim());

			var pagedAndFilteredObeEclDataLoanBooks = filteredObeEclDataLoanBooks
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclDataLoanBooks = from o in pagedAndFilteredObeEclDataLoanBooks
                         join o1 in _lookup_obeEclUploadRepository.GetAll() on o.ObeEclUploadId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclDataLoanBookForViewDto() {
							ObeEclDataLoanBook = new ObeEclDataLoanBookDto
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
                         	ObeEclUploadTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEclDataLoanBooks.CountAsync();

            return new PagedResultDto<GetObeEclDataLoanBookForViewDto>(
                totalCount,
                await obeEclDataLoanBooks.ToListAsync()
            );
         }
		 
		 public async Task<GetObeEclDataLoanBookForEditOutput> GetObeEclDataLoanBookForEdit(EntityDto<Guid> input)
         {
            var obeEclDataLoanBook = await _obeEclDataLoanBookRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclDataLoanBookForEditOutput {ObeEclDataLoanBook = ObjectMapper.Map<CreateOrEditObeEclDataLoanBookDto>(obeEclDataLoanBook)};

		    if (output.ObeEclDataLoanBook.ObeEclUploadId != null)
            {
                var _lookupObeEclUpload = await _lookup_obeEclUploadRepository.FirstOrDefaultAsync((Guid)output.ObeEclDataLoanBook.ObeEclUploadId);
                output.ObeEclUploadTenantId = _lookupObeEclUpload.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclDataLoanBookDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditObeEclDataLoanBookDto input)
         {
            var obeEclDataLoanBook = ObjectMapper.Map<ObeEclDataLoanBook>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEclDataLoanBook.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclDataLoanBookRepository.InsertAsync(obeEclDataLoanBook);
         }

		 protected virtual async Task Update(CreateOrEditObeEclDataLoanBookDto input)
         {
            var obeEclDataLoanBook = await _obeEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclDataLoanBook);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclDataLoanBookRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<ObeEclDataLoanBookObeEclUploadLookupTableDto>> GetAllObeEclUploadForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclUploadRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclUploadList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclDataLoanBookObeEclUploadLookupTableDto>();
			foreach(var obeEclUpload in obeEclUploadList){
				lookupTableDtoList.Add(new ObeEclDataLoanBookObeEclUploadLookupTableDto
				{
					Id = obeEclUpload.Id.ToString(),
					DisplayName = obeEclUpload.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclDataLoanBookObeEclUploadLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}