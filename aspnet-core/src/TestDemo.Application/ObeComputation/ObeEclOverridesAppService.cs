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
	[AbpAuthorize(AppPermissions.Pages_ObeEclOverrides)]
    public class ObeEclOverridesAppService : TestDemoAppServiceBase, IObeEclOverridesAppService
    {
		 private readonly IRepository<ObeEclOverride, Guid> _obeEclOverrideRepository;
		 private readonly IRepository<ObeEclDataLoanBook,Guid> _lookup_obeEclDataLoanBookRepository;
		 

		  public ObeEclOverridesAppService(IRepository<ObeEclOverride, Guid> obeEclOverrideRepository , IRepository<ObeEclDataLoanBook, Guid> lookup_obeEclDataLoanBookRepository) 
		  {
			_obeEclOverrideRepository = obeEclOverrideRepository;
			_lookup_obeEclDataLoanBookRepository = lookup_obeEclDataLoanBookRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclOverrideForViewDto>> GetAll(GetAllObeEclOverridesInput input)
         {
			
			var filteredObeEclOverrides = _obeEclOverrideRepository.GetAll()
						.Include( e => e.ObeEclDataLoanBookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  ||  e.ContractId.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclDataLoanBookCustomerNameFilter), e => e.ObeEclDataLoanBookFk != null && e.ObeEclDataLoanBookFk.CustomerName == input.ObeEclDataLoanBookCustomerNameFilter);

			var pagedAndFilteredObeEclOverrides = filteredObeEclOverrides
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclOverrides = from o in pagedAndFilteredObeEclOverrides
                         join o1 in _lookup_obeEclDataLoanBookRepository.GetAll() on o.ObeEclDataLoanBookId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclOverrideForViewDto() {
							ObeEclOverride = new ObeEclOverrideDto
							{
                                ContractId = o.ContractId,
                                Id = o.Id
							},
                         	ObeEclDataLoanBookCustomerName = s1 == null ? "" : s1.CustomerName.ToString()
						};

            var totalCount = await filteredObeEclOverrides.CountAsync();

            return new PagedResultDto<GetObeEclOverrideForViewDto>(
                totalCount,
                await obeEclOverrides.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEclOverrides_Edit)]
		 public async Task<GetObeEclOverrideForEditOutput> GetObeEclOverrideForEdit(EntityDto<Guid> input)
         {
            var obeEclOverride = await _obeEclOverrideRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclOverrideForEditOutput {ObeEclOverride = ObjectMapper.Map<CreateOrEditObeEclOverrideDto>(obeEclOverride)};

		    if (output.ObeEclOverride.ObeEclDataLoanBookId != null)
            {
                var _lookupObeEclDataLoanBook = await _lookup_obeEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.ObeEclOverride.ObeEclDataLoanBookId);
                output.ObeEclDataLoanBookCustomerName = _lookupObeEclDataLoanBook.CustomerName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclOverrideDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclOverrides_Create)]
		 protected virtual async Task Create(CreateOrEditObeEclOverrideDto input)
         {
            var obeEclOverride = ObjectMapper.Map<ObeEclOverride>(input);

			

            await _obeEclOverrideRepository.InsertAsync(obeEclOverride);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclOverrides_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEclOverrideDto input)
         {
            var obeEclOverride = await _obeEclOverrideRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclOverride);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclOverrides_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclOverrideRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEclOverrides)]
         public async Task<PagedResultDto<ObeEclOverrideObeEclDataLoanBookLookupTableDto>> GetAllObeEclDataLoanBookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclDataLoanBookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.CustomerName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclDataLoanBookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclOverrideObeEclDataLoanBookLookupTableDto>();
			foreach(var obeEclDataLoanBook in obeEclDataLoanBookList){
				lookupTableDtoList.Add(new ObeEclOverrideObeEclDataLoanBookLookupTableDto
				{
					Id = obeEclDataLoanBook.Id.ToString(),
					DisplayName = obeEclDataLoanBook.CustomerName?.ToString()
				});
			}

            return new PagedResultDto<ObeEclOverrideObeEclDataLoanBookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}