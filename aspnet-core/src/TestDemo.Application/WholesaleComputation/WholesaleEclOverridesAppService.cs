using TestDemo.WholesaleInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.WholesaleComputation
{
	[AbpAuthorize(AppPermissions.Pages_WholesaleEclOverrides)]
    public class WholesaleEclOverridesAppService : TestDemoAppServiceBase, IWholesaleEclOverridesAppService
    {
		 private readonly IRepository<WholesaleEclOverride, Guid> _wholesaleEclOverrideRepository;
		 private readonly IRepository<WholesaleEclDataLoanBook,Guid> _lookup_wholesaleEclDataLoanBookRepository;
		 

		  public WholesaleEclOverridesAppService(IRepository<WholesaleEclOverride, Guid> wholesaleEclOverrideRepository , IRepository<WholesaleEclDataLoanBook, Guid> lookup_wholesaleEclDataLoanBookRepository) 
		  {
			_wholesaleEclOverrideRepository = wholesaleEclOverrideRepository;
			_lookup_wholesaleEclDataLoanBookRepository = lookup_wholesaleEclDataLoanBookRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclOverrideForViewDto>> GetAll(GetAllWholesaleEclOverridesInput input)
         {
			
			var filteredWholesaleEclOverrides = _wholesaleEclOverrideRepository.GetAll()
						.Include( e => e.WholesaleEclDataLoanBookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Reason.Contains(input.Filter) || e.ContractId.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.WholesaleEclDataLoanBookCustomerNameFilter), e => e.WholesaleEclDataLoanBookFk != null && e.WholesaleEclDataLoanBookFk.CustomerName == input.WholesaleEclDataLoanBookCustomerNameFilter);

			var pagedAndFilteredWholesaleEclOverrides = filteredWholesaleEclOverrides
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclOverrides = from o in pagedAndFilteredWholesaleEclOverrides
                         join o1 in _lookup_wholesaleEclDataLoanBookRepository.GetAll() on o.WholesaleEclDataLoanBookId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclOverrideForViewDto() {
							WholesaleEclOverride = new WholesaleEclOverrideDto
							{
                                TtrYears = o.TtrYears,
                                FSV_Cash = o.FSV_Cash,
                                FSV_CommercialProperty = o.FSV_CommercialProperty,
                                FSV_Debenture = o.FSV_Debenture,
                                FSV_Inventory = o.FSV_Inventory,
                                FSV_PlantAndEquipment = o.FSV_PlantAndEquipment,
                                FSV_Receivables = o.FSV_Receivables,
                                FSV_ResidentialProperty = o.FSV_ResidentialProperty,
                                FSV_Shares = o.FSV_Shares,
                                FSV_Vehicle = o.FSV_Vehicle,
                                OverlaysPercentage = o.OverlaysPercentage,
                                Reason = o.Reason,
                                ContractId = o.ContractId,
                                Id = o.Id
							},
                         	WholesaleEclDataLoanBookCustomerName = s1 == null ? "" : s1.CustomerName.ToString()
						};

            var totalCount = await filteredWholesaleEclOverrides.CountAsync();

            return new PagedResultDto<GetWholesaleEclOverrideForViewDto>(
                totalCount,
                await wholesaleEclOverrides.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclOverrides_Edit)]
		 public async Task<GetWholesaleEclOverrideForEditOutput> GetWholesaleEclOverrideForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclOverride = await _wholesaleEclOverrideRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclOverrideForEditOutput {WholesaleEclOverride = ObjectMapper.Map<CreateOrEditWholesaleEclOverrideDto>(wholesaleEclOverride)};

		    if (output.WholesaleEclOverride.WholesaleEclDataLoanBookId != null)
            {
                var _lookupWholesaleEclDataLoanBook = await _lookup_wholesaleEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclOverride.WholesaleEclDataLoanBookId);
                output.WholesaleEclDataLoanBookCustomerName = _lookupWholesaleEclDataLoanBook.CustomerName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclOverrideDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclOverrides_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEclOverrideDto input)
         {
            var wholesaleEclOverride = ObjectMapper.Map<WholesaleEclOverride>(input);

			

            await _wholesaleEclOverrideRepository.InsertAsync(wholesaleEclOverride);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclOverrides_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEclOverrideDto input)
         {
            var wholesaleEclOverride = await _wholesaleEclOverrideRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclOverride);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclOverrides_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclOverrideRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEclOverrides)]
         public async Task<PagedResultDto<WholesaleEclOverrideWholesaleEclDataLoanBookLookupTableDto>> GetAllWholesaleEclDataLoanBookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclDataLoanBookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.CustomerName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclDataLoanBookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclOverrideWholesaleEclDataLoanBookLookupTableDto>();
			foreach(var wholesaleEclDataLoanBook in wholesaleEclDataLoanBookList){
				lookupTableDtoList.Add(new WholesaleEclOverrideWholesaleEclDataLoanBookLookupTableDto
				{
					Id = wholesaleEclDataLoanBook.Id.ToString(),
					DisplayName = wholesaleEclDataLoanBook.CustomerName?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclOverrideWholesaleEclDataLoanBookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}