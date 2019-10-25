using TestDemo.RetailInputs;

using TestDemo.EclShared;

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
	[AbpAuthorize(AppPermissions.Pages_RetailEclSicrs)]
    public class RetailEclSicrsAppService : TestDemoAppServiceBase, IRetailEclSicrsAppService
    {
		 private readonly IRepository<RetailEclSicr, Guid> _retailEclSicrRepository;
		 private readonly IRepository<RetailEclDataLoanBook,Guid> _lookup_retailEclDataLoanBookRepository;
		 

		  public RetailEclSicrsAppService(IRepository<RetailEclSicr, Guid> retailEclSicrRepository , IRepository<RetailEclDataLoanBook, Guid> lookup_retailEclDataLoanBookRepository) 
		  {
			_retailEclSicrRepository = retailEclSicrRepository;
			_lookup_retailEclDataLoanBookRepository = lookup_retailEclDataLoanBookRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclSicrForViewDto>> GetAll(GetAllRetailEclSicrsInput input)
         {
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredRetailEclSicrs = _retailEclSicrRepository.GetAll()
						.Include( e => e.RetailEclDataLoanBookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.OverrideSICR.Contains(input.Filter) || e.OverrideComment.Contains(input.Filter))
						.WhereIf(input.MinComputedSICRFilter != null, e => e.ComputedSICR >= input.MinComputedSICRFilter)
						.WhereIf(input.MaxComputedSICRFilter != null, e => e.ComputedSICR <= input.MaxComputedSICRFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.OverrideSICRFilter),  e => e.OverrideSICR.ToLower() == input.OverrideSICRFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.OverrideCommentFilter),  e => e.OverrideComment.ToLower() == input.OverrideCommentFilter.ToLower().Trim())
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.RetailEclDataLoanBookCustomerNameFilter), e => e.RetailEclDataLoanBookFk != null && e.RetailEclDataLoanBookFk.CustomerName.ToLower() == input.RetailEclDataLoanBookCustomerNameFilter.ToLower().Trim());

			var pagedAndFilteredRetailEclSicrs = filteredRetailEclSicrs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclSicrs = from o in pagedAndFilteredRetailEclSicrs
                         join o1 in _lookup_retailEclDataLoanBookRepository.GetAll() on o.RetailEclDataLoanBookId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEclSicrForViewDto() {
							RetailEclSicr = new RetailEclSicrDto
							{
                                ComputedSICR = o.ComputedSICR,
                                OverrideSICR = o.OverrideSICR,
                                OverrideComment = o.OverrideComment,
                                Status = o.Status,
                                Id = o.Id
							},
                         	RetailEclDataLoanBookCustomerName = s1 == null ? "" : s1.CustomerName.ToString()
						};

            var totalCount = await filteredRetailEclSicrs.CountAsync();

            return new PagedResultDto<GetRetailEclSicrForViewDto>(
                totalCount,
                await retailEclSicrs.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclSicrs_Edit)]
		 public async Task<GetRetailEclSicrForEditOutput> GetRetailEclSicrForEdit(EntityDto<Guid> input)
         {
            var retailEclSicr = await _retailEclSicrRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclSicrForEditOutput {RetailEclSicr = ObjectMapper.Map<CreateOrEditRetailEclSicrDto>(retailEclSicr)};

		    if (output.RetailEclSicr.RetailEclDataLoanBookId != null)
            {
                var _lookupRetailEclDataLoanBook = await _lookup_retailEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.RetailEclSicr.RetailEclDataLoanBookId);
                output.RetailEclDataLoanBookCustomerName = _lookupRetailEclDataLoanBook.CustomerName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclSicrDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclSicrs_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclSicrDto input)
         {
            var retailEclSicr = ObjectMapper.Map<RetailEclSicr>(input);

			
			if (AbpSession.TenantId != null)
			{
				retailEclSicr.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _retailEclSicrRepository.InsertAsync(retailEclSicr);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclSicrs_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclSicrDto input)
         {
            var retailEclSicr = await _retailEclSicrRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclSicr);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclSicrs_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclSicrRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclSicrs)]
         public async Task<PagedResultDto<RetailEclSicrRetailEclDataLoanBookLookupTableDto>> GetAllRetailEclDataLoanBookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclDataLoanBookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.CustomerName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclDataLoanBookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclSicrRetailEclDataLoanBookLookupTableDto>();
			foreach(var retailEclDataLoanBook in retailEclDataLoanBookList){
				lookupTableDtoList.Add(new RetailEclSicrRetailEclDataLoanBookLookupTableDto
				{
					Id = retailEclDataLoanBook.Id.ToString(),
					DisplayName = retailEclDataLoanBook.CustomerName?.ToString()
				});
			}

            return new PagedResultDto<RetailEclSicrRetailEclDataLoanBookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}