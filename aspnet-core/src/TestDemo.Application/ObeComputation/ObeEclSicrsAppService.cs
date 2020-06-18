using TestDemo.ObeInputs;

using TestDemo.EclShared;

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
    public class ObeEclSicrsAppService : TestDemoAppServiceBase, IObeEclSicrsAppService
    {
		 private readonly IRepository<ObeEclSicr, Guid> _obeEclSicrRepository;
		 private readonly IRepository<ObeEclDataLoanBook,Guid> _lookup_obeEclDataLoanBookRepository;
		 

		  public ObeEclSicrsAppService(IRepository<ObeEclSicr, Guid> obeEclSicrRepository , IRepository<ObeEclDataLoanBook, Guid> lookup_obeEclDataLoanBookRepository) 
		  {
			_obeEclSicrRepository = obeEclSicrRepository;
			_lookup_obeEclDataLoanBookRepository = lookup_obeEclDataLoanBookRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclSicrForViewDto>> GetAll(GetAllObeEclSicrsInput input)
         {
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredObeEclSicrs = _obeEclSicrRepository.GetAll()
						.Include( e => e.ObeEclDataLoanBookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.OverrideSICR.Contains(input.Filter) || e.OverrideComment.Contains(input.Filter))
						.WhereIf(input.MinComputedSICRFilter != null, e => e.ComputedSICR >= input.MinComputedSICRFilter)
						.WhereIf(input.MaxComputedSICRFilter != null, e => e.ComputedSICR <= input.MaxComputedSICRFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.OverrideSICRFilter),  e => e.OverrideSICR.ToLower() == input.OverrideSICRFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.OverrideCommentFilter),  e => e.OverrideComment.ToLower() == input.OverrideCommentFilter.ToLower().Trim())
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclDataLoanBookCustomerNameFilter), e => e.ObeEclDataLoanBookFk != null && e.ObeEclDataLoanBookFk.CustomerName.ToLower() == input.ObeEclDataLoanBookCustomerNameFilter.ToLower().Trim());

			var pagedAndFilteredObeEclSicrs = filteredObeEclSicrs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclSicrs = from o in pagedAndFilteredObeEclSicrs
                         join o1 in _lookup_obeEclDataLoanBookRepository.GetAll() on o.ObeEclDataLoanBookId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclSicrForViewDto() {
							ObeEclSicr = new ObeEclSicrDto
							{
                                ComputedSICR = o.ComputedSICR,
                                OverrideSICR = o.OverrideSICR,
                                OverrideComment = o.OverrideComment,
                                Status = o.Status,
                                Id = o.Id
							},
                         	ObeEclDataLoanBookCustomerName = s1 == null ? "" : s1.CustomerName.ToString()
						};

            var totalCount = await filteredObeEclSicrs.CountAsync();

            return new PagedResultDto<GetObeEclSicrForViewDto>(
                totalCount,
                await obeEclSicrs.ToListAsync()
            );
         }
		 
		 public async Task<GetObeEclSicrForEditOutput> GetObeEclSicrForEdit(EntityDto<Guid> input)
         {
            var obeEclSicr = await _obeEclSicrRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclSicrForEditOutput {ObeEclSicr = ObjectMapper.Map<CreateOrEditObeEclSicrDto>(obeEclSicr)};

		    if (output.ObeEclSicr.ObeEclDataLoanBookId != null)
            {
                var _lookupObeEclDataLoanBook = await _lookup_obeEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.ObeEclSicr.ObeEclDataLoanBookId);
                output.ObeEclDataLoanBookCustomerName = _lookupObeEclDataLoanBook.CustomerName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclSicrDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditObeEclSicrDto input)
         {
            var obeEclSicr = ObjectMapper.Map<ObeEclSicr>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEclSicr.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclSicrRepository.InsertAsync(obeEclSicr);
         }

		 protected virtual async Task Update(CreateOrEditObeEclSicrDto input)
         {
            var obeEclSicr = await _obeEclSicrRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclSicr);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclSicrRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<ObeEclSicrObeEclDataLoanBookLookupTableDto>> GetAllObeEclDataLoanBookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclDataLoanBookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.CustomerName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclDataLoanBookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclSicrObeEclDataLoanBookLookupTableDto>();
			foreach(var obeEclDataLoanBook in obeEclDataLoanBookList){
				lookupTableDtoList.Add(new ObeEclSicrObeEclDataLoanBookLookupTableDto
				{
					Id = obeEclDataLoanBook.Id.ToString(),
					DisplayName = obeEclDataLoanBook.CustomerName?.ToString()
				});
			}

            return new PagedResultDto<ObeEclSicrObeEclDataLoanBookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}