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
	[AbpAuthorize(AppPermissions.Pages_WholesaleEclComputedEadResults)]
    public class WholesaleEclComputedEadResultsAppService : TestDemoAppServiceBase, IWholesaleEclComputedEadResultsAppService
    {
		 private readonly IRepository<WholesaleEclComputedEadResult, Guid> _wholesaleEclComputedEadResultRepository;
		 private readonly IRepository<WholesaleEclDataLoanBook,Guid> _lookup_wholesaleEclDataLoanBookRepository;
		 

		  public WholesaleEclComputedEadResultsAppService(IRepository<WholesaleEclComputedEadResult, Guid> wholesaleEclComputedEadResultRepository , IRepository<WholesaleEclDataLoanBook, Guid> lookup_wholesaleEclDataLoanBookRepository) 
		  {
			_wholesaleEclComputedEadResultRepository = wholesaleEclComputedEadResultRepository;
			_lookup_wholesaleEclDataLoanBookRepository = lookup_wholesaleEclDataLoanBookRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclComputedEadResultForViewDto>> GetAll(GetAllWholesaleEclComputedEadResultsInput input)
         {
			
			var filteredWholesaleEclComputedEadResults = _wholesaleEclComputedEadResultRepository.GetAll()
						.Include( e => e.WholesaleEclDataLoanBookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.LifetimeEAD.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.LifetimeEADFilter),  e => e.LifetimeEAD.ToLower() == input.LifetimeEADFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.WholesaleEclDataLoanBookCustomerNameFilter), e => e.WholesaleEclDataLoanBookFk != null && e.WholesaleEclDataLoanBookFk.CustomerName.ToLower() == input.WholesaleEclDataLoanBookCustomerNameFilter.ToLower().Trim());

			var pagedAndFilteredWholesaleEclComputedEadResults = filteredWholesaleEclComputedEadResults
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclComputedEadResults = from o in pagedAndFilteredWholesaleEclComputedEadResults
                         join o1 in _lookup_wholesaleEclDataLoanBookRepository.GetAll() on o.WholesaleEclDataLoanBookId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclComputedEadResultForViewDto() {
							WholesaleEclComputedEadResult = new WholesaleEclComputedEadResultDto
							{
                                LifetimeEAD = o.LifetimeEAD,
                                Id = o.Id
							},
                         	WholesaleEclDataLoanBookCustomerName = s1 == null ? "" : s1.CustomerName.ToString()
						};

            var totalCount = await filteredWholesaleEclComputedEadResults.CountAsync();

            return new PagedResultDto<GetWholesaleEclComputedEadResultForViewDto>(
                totalCount,
                await wholesaleEclComputedEadResults.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclComputedEadResults_Edit)]
		 public async Task<GetWholesaleEclComputedEadResultForEditOutput> GetWholesaleEclComputedEadResultForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclComputedEadResult = await _wholesaleEclComputedEadResultRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclComputedEadResultForEditOutput {WholesaleEclComputedEadResult = ObjectMapper.Map<CreateOrEditWholesaleEclComputedEadResultDto>(wholesaleEclComputedEadResult)};

		    if (output.WholesaleEclComputedEadResult.WholesaleEclDataLoanBookId != null)
            {
                var _lookupWholesaleEclDataLoanBook = await _lookup_wholesaleEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclComputedEadResult.WholesaleEclDataLoanBookId);
                output.WholesaleEclDataLoanBookCustomerName = _lookupWholesaleEclDataLoanBook.CustomerName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclComputedEadResultDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclComputedEadResults_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEclComputedEadResultDto input)
         {
            var wholesaleEclComputedEadResult = ObjectMapper.Map<WholesaleEclComputedEadResult>(input);

			
			if (AbpSession.TenantId != null)
			{
				wholesaleEclComputedEadResult.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _wholesaleEclComputedEadResultRepository.InsertAsync(wholesaleEclComputedEadResult);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclComputedEadResults_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEclComputedEadResultDto input)
         {
            var wholesaleEclComputedEadResult = await _wholesaleEclComputedEadResultRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclComputedEadResult);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclComputedEadResults_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclComputedEadResultRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEclComputedEadResults)]
         public async Task<PagedResultDto<WholesaleEclComputedEadResultWholesaleEclDataLoanBookLookupTableDto>> GetAllWholesaleEclDataLoanBookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclDataLoanBookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.CustomerName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclDataLoanBookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclComputedEadResultWholesaleEclDataLoanBookLookupTableDto>();
			foreach(var wholesaleEclDataLoanBook in wholesaleEclDataLoanBookList){
				lookupTableDtoList.Add(new WholesaleEclComputedEadResultWholesaleEclDataLoanBookLookupTableDto
				{
					Id = wholesaleEclDataLoanBook.Id.ToString(),
					DisplayName = wholesaleEclDataLoanBook.CustomerName?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclComputedEadResultWholesaleEclDataLoanBookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}