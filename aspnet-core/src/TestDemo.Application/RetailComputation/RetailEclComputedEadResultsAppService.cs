using TestDemo.RetailInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.RetailComputation
{
	[AbpAuthorize(AppPermissions.Pages_RetailEclComputedEadResults)]
    public class RetailEclComputedEadResultsAppService : TestDemoAppServiceBase, IRetailEclComputedEadResultsAppService
    {
		 private readonly IRepository<RetailEclComputedEadResult, Guid> _retailEclComputedEadResultRepository;
		 private readonly IRepository<RetailEclDataLoanBook,Guid> _lookup_retailEclDataLoanBookRepository;
		 

		  public RetailEclComputedEadResultsAppService(IRepository<RetailEclComputedEadResult, Guid> retailEclComputedEadResultRepository , IRepository<RetailEclDataLoanBook, Guid> lookup_retailEclDataLoanBookRepository) 
		  {
			_retailEclComputedEadResultRepository = retailEclComputedEadResultRepository;
			_lookup_retailEclDataLoanBookRepository = lookup_retailEclDataLoanBookRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclComputedEadResultForViewDto>> GetAll(GetAllRetailEclComputedEadResultsInput input)
         {
			
			var filteredRetailEclComputedEadResults = _retailEclComputedEadResultRepository.GetAll()
						.Include( e => e.RetailEclDataLoanBookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.LifetimeEAD.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.LifetimeEADFilter),  e => e.LifetimeEAD.ToLower() == input.LifetimeEADFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.RetailEclDataLoanBookCustomerNameFilter), e => e.RetailEclDataLoanBookFk != null && e.RetailEclDataLoanBookFk.CustomerName.ToLower() == input.RetailEclDataLoanBookCustomerNameFilter.ToLower().Trim());

			var pagedAndFilteredRetailEclComputedEadResults = filteredRetailEclComputedEadResults
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclComputedEadResults = from o in pagedAndFilteredRetailEclComputedEadResults
                         join o1 in _lookup_retailEclDataLoanBookRepository.GetAll() on o.RetailEclDataLoanBookId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEclComputedEadResultForViewDto() {
							RetailEclComputedEadResult = new RetailEclComputedEadResultDto
							{
                                LifetimeEAD = o.LifetimeEAD,
                                Id = o.Id
							},
                         	RetailEclDataLoanBookCustomerName = s1 == null ? "" : s1.CustomerName.ToString()
						};

            var totalCount = await filteredRetailEclComputedEadResults.CountAsync();

            return new PagedResultDto<GetRetailEclComputedEadResultForViewDto>(
                totalCount,
                await retailEclComputedEadResults.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclComputedEadResults_Edit)]
		 public async Task<GetRetailEclComputedEadResultForEditOutput> GetRetailEclComputedEadResultForEdit(EntityDto<Guid> input)
         {
            var retailEclComputedEadResult = await _retailEclComputedEadResultRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclComputedEadResultForEditOutput {RetailEclComputedEadResult = ObjectMapper.Map<CreateOrEditRetailEclComputedEadResultDto>(retailEclComputedEadResult)};

		    if (output.RetailEclComputedEadResult.RetailEclDataLoanBookId != null)
            {
                var _lookupRetailEclDataLoanBook = await _lookup_retailEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.RetailEclComputedEadResult.RetailEclDataLoanBookId);
                output.RetailEclDataLoanBookCustomerName = _lookupRetailEclDataLoanBook.CustomerName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclComputedEadResultDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclComputedEadResults_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclComputedEadResultDto input)
         {
            var retailEclComputedEadResult = ObjectMapper.Map<RetailEclComputedEadResult>(input);

			
			if (AbpSession.TenantId != null)
			{
				retailEclComputedEadResult.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _retailEclComputedEadResultRepository.InsertAsync(retailEclComputedEadResult);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclComputedEadResults_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclComputedEadResultDto input)
         {
            var retailEclComputedEadResult = await _retailEclComputedEadResultRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclComputedEadResult);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclComputedEadResults_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclComputedEadResultRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclComputedEadResults)]
         public async Task<PagedResultDto<RetailEclComputedEadResultRetailEclDataLoanBookLookupTableDto>> GetAllRetailEclDataLoanBookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclDataLoanBookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.CustomerName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclDataLoanBookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclComputedEadResultRetailEclDataLoanBookLookupTableDto>();
			foreach(var retailEclDataLoanBook in retailEclDataLoanBookList){
				lookupTableDtoList.Add(new RetailEclComputedEadResultRetailEclDataLoanBookLookupTableDto
				{
					Id = retailEclDataLoanBook.Id.ToString(),
					DisplayName = retailEclDataLoanBook.CustomerName?.ToString()
				});
			}

            return new PagedResultDto<RetailEclComputedEadResultRetailEclDataLoanBookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}