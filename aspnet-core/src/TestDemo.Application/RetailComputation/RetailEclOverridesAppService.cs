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
	[AbpAuthorize(AppPermissions.Pages_RetailEclOverrides)]
    public class RetailEclOverridesAppService : TestDemoAppServiceBase, IRetailEclOverridesAppService
    {
		 private readonly IRepository<RetailEclOverride, Guid> _retailEclOverrideRepository;
		 private readonly IRepository<RetailEclDataLoanBook,Guid> _lookup_retailEclDataLoanBookRepository;
		 

		  public RetailEclOverridesAppService(IRepository<RetailEclOverride, Guid> retailEclOverrideRepository , IRepository<RetailEclDataLoanBook, Guid> lookup_retailEclDataLoanBookRepository) 
		  {
			_retailEclOverrideRepository = retailEclOverrideRepository;
			_lookup_retailEclDataLoanBookRepository = lookup_retailEclDataLoanBookRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclOverrideForViewDto>> GetAll(GetAllRetailEclOverridesInput input)
         {
			
			var filteredRetailEclOverrides = _retailEclOverrideRepository.GetAll()
						.Include( e => e.RetailEclDataLoanBookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ContractId.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.RetailEclDataLoanBookCustomerNameFilter), e => e.RetailEclDataLoanBookFk != null && e.RetailEclDataLoanBookFk.CustomerName == input.RetailEclDataLoanBookCustomerNameFilter);

			var pagedAndFilteredRetailEclOverrides = filteredRetailEclOverrides
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclOverrides = from o in pagedAndFilteredRetailEclOverrides
                         join o1 in _lookup_retailEclDataLoanBookRepository.GetAll() on o.RetailEclDataLoanBookId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEclOverrideForViewDto() {
							RetailEclOverride = new RetailEclOverrideDto
							{
                                ContractId = o.ContractId,
                                Id = o.Id
							},
                         	RetailEclDataLoanBookCustomerName = s1 == null ? "" : s1.CustomerName.ToString()
						};

            var totalCount = await filteredRetailEclOverrides.CountAsync();

            return new PagedResultDto<GetRetailEclOverrideForViewDto>(
                totalCount,
                await retailEclOverrides.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclOverrides_Edit)]
		 public async Task<GetRetailEclOverrideForEditOutput> GetRetailEclOverrideForEdit(EntityDto<Guid> input)
         {
            var retailEclOverride = await _retailEclOverrideRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclOverrideForEditOutput {RetailEclOverride = ObjectMapper.Map<CreateOrEditRetailEclOverrideDto>(retailEclOverride)};

		    if (output.RetailEclOverride.RetailEclDataLoanBookId != null)
            {
                var _lookupRetailEclDataLoanBook = await _lookup_retailEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.RetailEclOverride.RetailEclDataLoanBookId);
                output.RetailEclDataLoanBookCustomerName = _lookupRetailEclDataLoanBook.CustomerName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclOverrideDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclOverrides_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclOverrideDto input)
         {
            var retailEclOverride = ObjectMapper.Map<RetailEclOverride>(input);

			

            await _retailEclOverrideRepository.InsertAsync(retailEclOverride);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclOverrides_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclOverrideDto input)
         {
            var retailEclOverride = await _retailEclOverrideRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclOverride);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclOverrides_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclOverrideRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclOverrides)]
         public async Task<PagedResultDto<RetailEclOverrideRetailEclDataLoanBookLookupTableDto>> GetAllRetailEclDataLoanBookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclDataLoanBookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.CustomerName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclDataLoanBookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclOverrideRetailEclDataLoanBookLookupTableDto>();
			foreach(var retailEclDataLoanBook in retailEclDataLoanBookList){
				lookupTableDtoList.Add(new RetailEclOverrideRetailEclDataLoanBookLookupTableDto
				{
					Id = retailEclDataLoanBook.Id.ToString(),
					DisplayName = retailEclDataLoanBook.CustomerName?.ToString()
				});
			}

            return new PagedResultDto<RetailEclOverrideRetailEclDataLoanBookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}