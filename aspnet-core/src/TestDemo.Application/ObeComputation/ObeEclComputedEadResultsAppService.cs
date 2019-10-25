using TestDemo.ObeInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.ObeComputation
{
	[AbpAuthorize(AppPermissions.Pages_ObeEclComputedEadResults)]
    public class ObeEclComputedEadResultsAppService : TestDemoAppServiceBase, IObeEclComputedEadResultsAppService
    {
		 private readonly IRepository<ObeEclComputedEadResult, Guid> _obeEclComputedEadResultRepository;
		 private readonly IRepository<ObeEclDataLoanBook,Guid> _lookup_obeEclDataLoanBookRepository;
		 

		  public ObeEclComputedEadResultsAppService(IRepository<ObeEclComputedEadResult, Guid> obeEclComputedEadResultRepository , IRepository<ObeEclDataLoanBook, Guid> lookup_obeEclDataLoanBookRepository) 
		  {
			_obeEclComputedEadResultRepository = obeEclComputedEadResultRepository;
			_lookup_obeEclDataLoanBookRepository = lookup_obeEclDataLoanBookRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclComputedEadResultForViewDto>> GetAll(GetAllObeEclComputedEadResultsInput input)
         {
			
			var filteredObeEclComputedEadResults = _obeEclComputedEadResultRepository.GetAll()
						.Include( e => e.ObeEclDataLoanBookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.LifetimeEAD.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.LifetimeEADFilter),  e => e.LifetimeEAD.ToLower() == input.LifetimeEADFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclDataLoanBookContractNoFilter), e => e.ObeEclDataLoanBookFk != null && e.ObeEclDataLoanBookFk.ContractNo.ToLower() == input.ObeEclDataLoanBookContractNoFilter.ToLower().Trim());

			var pagedAndFilteredObeEclComputedEadResults = filteredObeEclComputedEadResults
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclComputedEadResults = from o in pagedAndFilteredObeEclComputedEadResults
                         join o1 in _lookup_obeEclDataLoanBookRepository.GetAll() on o.ObeEclDataLoanBookId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclComputedEadResultForViewDto() {
							ObeEclComputedEadResult = new ObeEclComputedEadResultDto
							{
                                LifetimeEAD = o.LifetimeEAD,
                                Id = o.Id
							},
                         	ObeEclDataLoanBookContractNo = s1 == null ? "" : s1.ContractNo.ToString()
						};

            var totalCount = await filteredObeEclComputedEadResults.CountAsync();

            return new PagedResultDto<GetObeEclComputedEadResultForViewDto>(
                totalCount,
                await obeEclComputedEadResults.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEclComputedEadResults_Edit)]
		 public async Task<GetObeEclComputedEadResultForEditOutput> GetObeEclComputedEadResultForEdit(EntityDto<Guid> input)
         {
            var obeEclComputedEadResult = await _obeEclComputedEadResultRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclComputedEadResultForEditOutput {ObeEclComputedEadResult = ObjectMapper.Map<CreateOrEditObeEclComputedEadResultDto>(obeEclComputedEadResult)};

		    if (output.ObeEclComputedEadResult.ObeEclDataLoanBookId != null)
            {
                var _lookupObeEclDataLoanBook = await _lookup_obeEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.ObeEclComputedEadResult.ObeEclDataLoanBookId);
                output.ObeEclDataLoanBookContractNo = _lookupObeEclDataLoanBook.ContractNo.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclComputedEadResultDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclComputedEadResults_Create)]
		 protected virtual async Task Create(CreateOrEditObeEclComputedEadResultDto input)
         {
            var obeEclComputedEadResult = ObjectMapper.Map<ObeEclComputedEadResult>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEclComputedEadResult.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclComputedEadResultRepository.InsertAsync(obeEclComputedEadResult);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclComputedEadResults_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEclComputedEadResultDto input)
         {
            var obeEclComputedEadResult = await _obeEclComputedEadResultRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclComputedEadResult);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclComputedEadResults_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclComputedEadResultRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEclComputedEadResults)]
         public async Task<PagedResultDto<ObeEclComputedEadResultObeEclDataLoanBookLookupTableDto>> GetAllObeEclDataLoanBookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclDataLoanBookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.ContractNo.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclDataLoanBookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclComputedEadResultObeEclDataLoanBookLookupTableDto>();
			foreach(var obeEclDataLoanBook in obeEclDataLoanBookList){
				lookupTableDtoList.Add(new ObeEclComputedEadResultObeEclDataLoanBookLookupTableDto
				{
					Id = obeEclDataLoanBook.Id.ToString(),
					DisplayName = obeEclDataLoanBook.ContractNo?.ToString()
				});
			}

            return new PagedResultDto<ObeEclComputedEadResultObeEclDataLoanBookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}