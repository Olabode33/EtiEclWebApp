using TestDemo.Retail;
using TestDemo.RetailInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailResults.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.RetailResults
{
    public class RetailEclResultSummaryTopExposuresAppService : TestDemoAppServiceBase, IRetailEclResultSummaryTopExposuresAppService
    {
		 private readonly IRepository<RetailEclResultSummaryTopExposure, Guid> _retailEclResultSummaryTopExposureRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 private readonly IRepository<RetailEclDataLoanBook,Guid> _lookup_retailEclDataLoanBookRepository;
		 

		  public RetailEclResultSummaryTopExposuresAppService(IRepository<RetailEclResultSummaryTopExposure, Guid> retailEclResultSummaryTopExposureRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository, IRepository<RetailEclDataLoanBook, Guid> lookup_retailEclDataLoanBookRepository) 
		  {
			_retailEclResultSummaryTopExposureRepository = retailEclResultSummaryTopExposureRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		_lookup_retailEclDataLoanBookRepository = lookup_retailEclDataLoanBookRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclResultSummaryTopExposureForViewDto>> GetAll(GetAllRetailEclResultSummaryTopExposuresInput input)
         {
			
			var filteredRetailEclResultSummaryTopExposures = _retailEclResultSummaryTopExposureRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.Include( e => e.RetailEclDataLoanBookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.MinPreOverrideExposureFilter != null, e => e.PreOverrideExposure >= input.MinPreOverrideExposureFilter)
						.WhereIf(input.MaxPreOverrideExposureFilter != null, e => e.PreOverrideExposure <= input.MaxPreOverrideExposureFilter)
						.WhereIf(input.MinPreOverrideImpairmentFilter != null, e => e.PreOverrideImpairment >= input.MinPreOverrideImpairmentFilter)
						.WhereIf(input.MaxPreOverrideImpairmentFilter != null, e => e.PreOverrideImpairment <= input.MaxPreOverrideImpairmentFilter)
						.WhereIf(input.MinPreOverrideCoverageRatioFilter != null, e => e.PreOverrideCoverageRatio >= input.MinPreOverrideCoverageRatioFilter)
						.WhereIf(input.MaxPreOverrideCoverageRatioFilter != null, e => e.PreOverrideCoverageRatio <= input.MaxPreOverrideCoverageRatioFilter)
						.WhereIf(input.MinPostOverrideExposureFilter != null, e => e.PostOverrideExposure >= input.MinPostOverrideExposureFilter)
						.WhereIf(input.MaxPostOverrideExposureFilter != null, e => e.PostOverrideExposure <= input.MaxPostOverrideExposureFilter)
						.WhereIf(input.MinPostOverrideImpairmentFilter != null, e => e.PostOverrideImpairment >= input.MinPostOverrideImpairmentFilter)
						.WhereIf(input.MaxPostOverrideImpairmentFilter != null, e => e.PostOverrideImpairment <= input.MaxPostOverrideImpairmentFilter)
						.WhereIf(input.MinPostOverrideCoverageRatioFilter != null, e => e.PostOverrideCoverageRatio >= input.MinPostOverrideCoverageRatioFilter)
						.WhereIf(input.MaxPostOverrideCoverageRatioFilter != null, e => e.PostOverrideCoverageRatio <= input.MaxPostOverrideCoverageRatioFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.RetailEclDataLoanBookContractNoFilter), e => e.RetailEclDataLoanBookFk != null && e.RetailEclDataLoanBookFk.ContractNo.ToLower() == input.RetailEclDataLoanBookContractNoFilter.ToLower().Trim());

			var pagedAndFilteredRetailEclResultSummaryTopExposures = filteredRetailEclResultSummaryTopExposures
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclResultSummaryTopExposures = from o in pagedAndFilteredRetailEclResultSummaryTopExposures
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_retailEclDataLoanBookRepository.GetAll() on o.RetailEclDataLoanBookId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetRetailEclResultSummaryTopExposureForViewDto() {
							RetailEclResultSummaryTopExposure = new RetailEclResultSummaryTopExposureDto
							{
                                PreOverrideExposure = o.PreOverrideExposure,
                                PreOverrideImpairment = o.PreOverrideImpairment,
                                PreOverrideCoverageRatio = o.PreOverrideCoverageRatio,
                                PostOverrideExposure = o.PostOverrideExposure,
                                PostOverrideImpairment = o.PostOverrideImpairment,
                                PostOverrideCoverageRatio = o.PostOverrideCoverageRatio,
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString(),
                         	RetailEclDataLoanBookContractNo = s2 == null ? "" : s2.ContractNo.ToString()
						};

            var totalCount = await filteredRetailEclResultSummaryTopExposures.CountAsync();

            return new PagedResultDto<GetRetailEclResultSummaryTopExposureForViewDto>(
                totalCount,
                await retailEclResultSummaryTopExposures.ToListAsync()
            );
         }
		 
		 public async Task<GetRetailEclResultSummaryTopExposureForEditOutput> GetRetailEclResultSummaryTopExposureForEdit(EntityDto<Guid> input)
         {
            var retailEclResultSummaryTopExposure = await _retailEclResultSummaryTopExposureRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclResultSummaryTopExposureForEditOutput {RetailEclResultSummaryTopExposure = ObjectMapper.Map<CreateOrEditRetailEclResultSummaryTopExposureDto>(retailEclResultSummaryTopExposure)};

		    if (output.RetailEclResultSummaryTopExposure.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclResultSummaryTopExposure.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }

		    if (output.RetailEclResultSummaryTopExposure.RetailEclDataLoanBookId != null)
            {
                var _lookupRetailEclDataLoanBook = await _lookup_retailEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.RetailEclResultSummaryTopExposure.RetailEclDataLoanBookId);
                output.RetailEclDataLoanBookContractNo = _lookupRetailEclDataLoanBook.ContractNo.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclResultSummaryTopExposureDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditRetailEclResultSummaryTopExposureDto input)
         {
            var retailEclResultSummaryTopExposure = ObjectMapper.Map<RetailEclResultSummaryTopExposure>(input);

			
			if (AbpSession.TenantId != null)
			{
				retailEclResultSummaryTopExposure.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _retailEclResultSummaryTopExposureRepository.InsertAsync(retailEclResultSummaryTopExposure);
         }

		 protected virtual async Task Update(CreateOrEditRetailEclResultSummaryTopExposureDto input)
         {
            var retailEclResultSummaryTopExposure = await _retailEclResultSummaryTopExposureRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclResultSummaryTopExposure);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclResultSummaryTopExposureRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<RetailEclResultSummaryTopExposureRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclResultSummaryTopExposureRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailEclResultSummaryTopExposureRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEclResultSummaryTopExposureRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

         public async Task<PagedResultDto<RetailEclResultSummaryTopExposureRetailEclDataLoanBookLookupTableDto>> GetAllRetailEclDataLoanBookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclDataLoanBookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.ContractNo.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclDataLoanBookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclResultSummaryTopExposureRetailEclDataLoanBookLookupTableDto>();
			foreach(var retailEclDataLoanBook in retailEclDataLoanBookList){
				lookupTableDtoList.Add(new RetailEclResultSummaryTopExposureRetailEclDataLoanBookLookupTableDto
				{
					Id = retailEclDataLoanBook.Id.ToString(),
					DisplayName = retailEclDataLoanBook.ContractNo?.ToString()
				});
			}

            return new PagedResultDto<RetailEclResultSummaryTopExposureRetailEclDataLoanBookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}