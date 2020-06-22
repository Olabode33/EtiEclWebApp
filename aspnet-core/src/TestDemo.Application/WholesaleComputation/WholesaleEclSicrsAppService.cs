using TestDemo.WholesaleInputs;

using TestDemo.EclShared;

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
    public class WholesaleEclSicrsAppService : TestDemoAppServiceBase, IWholesaleEclSicrsAppService
    {
		 private readonly IRepository<WholesaleEclSicr, Guid> _wholesaleEclSicrRepository;
		 private readonly IRepository<WholesaleEclDataLoanBook,Guid> _lookup_wholesaleEclDataLoanBookRepository;
		 

		  public WholesaleEclSicrsAppService(IRepository<WholesaleEclSicr, Guid> wholesaleEclSicrRepository , IRepository<WholesaleEclDataLoanBook, Guid> lookup_wholesaleEclDataLoanBookRepository) 
		  {
			_wholesaleEclSicrRepository = wholesaleEclSicrRepository;
			_lookup_wholesaleEclDataLoanBookRepository = lookup_wholesaleEclDataLoanBookRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclSicrForViewDto>> GetAll(GetAllWholesaleEclSicrsInput input)
         {
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;

            var filteredWholesaleEclSicrs = _wholesaleEclSicrRepository.GetAll()
                        //.Include( e => e.WholesaleEclDataLoanBookFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.OverrideSICR.Contains(input.Filter) || e.OverrideComment.Contains(input.Filter))
                        .WhereIf(input.MinComputedSICRFilter != null, e => e.ComputedSICR >= input.MinComputedSICRFilter)
                        .WhereIf(input.MaxComputedSICRFilter != null, e => e.ComputedSICR <= input.MaxComputedSICRFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OverrideSICRFilter), e => e.OverrideSICR.ToLower() == input.OverrideSICRFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OverrideCommentFilter), e => e.OverrideComment.ToLower() == input.OverrideCommentFilter.ToLower().Trim())
                        .WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter);
						//.WhereIf(!string.IsNullOrWhiteSpace(input.WholesaleEclDataLoanBookContractNoFilter), e => e.WholesaleEclDataLoanBookFk != null && e.WholesaleEclDataLoanBookFk.ContractNo.ToLower() == input.WholesaleEclDataLoanBookContractNoFilter.ToLower().Trim());

			var pagedAndFilteredWholesaleEclSicrs = filteredWholesaleEclSicrs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclSicrs = from o in pagedAndFilteredWholesaleEclSicrs
                         join o1 in _lookup_wholesaleEclDataLoanBookRepository.GetAll() on o.WholesaleEclDataLoanBookId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclSicrForViewDto() {
							WholesaleEclSicr = new WholesaleEclSicrDto
							{
                                ComputedSICR = o.ComputedSICR,
                                OverrideSICR = o.OverrideSICR,
                                OverrideComment = o.OverrideComment,
                                Status = o.Status,
                                Id = o.Id
							},
                         	WholesaleEclDataLoanBookContractNo = s1 == null ? "" : s1.ContractNo.ToString()
						};

            var totalCount = await filteredWholesaleEclSicrs.CountAsync();

            return new PagedResultDto<GetWholesaleEclSicrForViewDto>(
                totalCount,
                await wholesaleEclSicrs.ToListAsync()
            );
         }
		 
		 public async Task<GetWholesaleEclSicrForEditOutput> GetWholesaleEclSicrForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclSicr = await _wholesaleEclSicrRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclSicrForEditOutput {WholesaleEclSicr = ObjectMapper.Map<CreateOrEditWholesaleEclSicrDto>(wholesaleEclSicr)};

		    if (output.WholesaleEclSicr.WholesaleEclDataLoanBookId != null)
            {
                var _lookupWholesaleEclDataLoanBook = await _lookup_wholesaleEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclSicr.WholesaleEclDataLoanBookId);
                output.WholesaleEclDataLoanBookContractNo = _lookupWholesaleEclDataLoanBook.ContractNo.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclSicrDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditWholesaleEclSicrDto input)
         {
            var wholesaleEclSicr = ObjectMapper.Map<WholesaleEclSicr>(input);

			
			if (AbpSession.TenantId != null)
			{
				wholesaleEclSicr.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _wholesaleEclSicrRepository.InsertAsync(wholesaleEclSicr);
         }

		 protected virtual async Task Update(CreateOrEditWholesaleEclSicrDto input)
         {
            var wholesaleEclSicr = await _wholesaleEclSicrRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclSicr);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclSicrRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<WholesaleEclSicrWholesaleEclDataLoanBookLookupTableDto>> GetAllWholesaleEclDataLoanBookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclDataLoanBookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.ContractNo.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclDataLoanBookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclSicrWholesaleEclDataLoanBookLookupTableDto>();
			foreach(var wholesaleEclDataLoanBook in wholesaleEclDataLoanBookList){
				lookupTableDtoList.Add(new WholesaleEclSicrWholesaleEclDataLoanBookLookupTableDto
				{
					Id = wholesaleEclDataLoanBook.Id.ToString(),
					DisplayName = wholesaleEclDataLoanBook.ContractNo?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclSicrWholesaleEclDataLoanBookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}