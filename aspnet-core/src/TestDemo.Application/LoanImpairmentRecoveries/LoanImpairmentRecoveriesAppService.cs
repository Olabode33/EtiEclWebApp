

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.LoanImpairmentRecoveries.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.LoanImpairmentRecoveries
{
	[AbpAuthorize(AppPermissions.Pages_LoanImpairmentRecoveries)]
    public class LoanImpairmentRecoveriesAppService : TestDemoAppServiceBase, ILoanImpairmentRecoveriesAppService
    {
		 private readonly IRepository<LoanImpairmentRecovery, Guid> _loanImpairmentRecoveryRepository;
		 

		  public LoanImpairmentRecoveriesAppService(IRepository<LoanImpairmentRecovery, Guid> loanImpairmentRecoveryRepository ) 
		  {
			_loanImpairmentRecoveryRepository = loanImpairmentRecoveryRepository;
			
		  }

		 public async Task<PagedResultDto<GetLoanImpairmentRecoveryForViewDto>> GetAll(GetAllLoanImpairmentRecoveriesInput input)
         {
			
			var filteredLoanImpairmentRecoveries = _loanImpairmentRecoveryRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Recovery.Contains(input.Filter));

			var pagedAndFilteredLoanImpairmentRecoveries = filteredLoanImpairmentRecoveries
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var loanImpairmentRecoveries = from o in pagedAndFilteredLoanImpairmentRecoveries
                         select new GetLoanImpairmentRecoveryForViewDto() {
							LoanImpairmentRecovery = new LoanImpairmentRecoveryDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredLoanImpairmentRecoveries.CountAsync();

            return new PagedResultDto<GetLoanImpairmentRecoveryForViewDto>(
                totalCount,
                await loanImpairmentRecoveries.ToListAsync()
            );
         }
		 
		 public async Task<GetLoanImpairmentRecoveryForViewDto> GetLoanImpairmentRecoveryForView(Guid id)
         {
            var loanImpairmentRecovery = await _loanImpairmentRecoveryRepository.GetAsync(id);

            var output = new GetLoanImpairmentRecoveryForViewDto { LoanImpairmentRecovery = ObjectMapper.Map<LoanImpairmentRecoveryDto>(loanImpairmentRecovery) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentRecoveries_Edit)]
		 public async Task<GetLoanImpairmentRecoveryForEditOutput> GetLoanImpairmentRecoveryForEdit(EntityDto<Guid> input)
         {
            var loanImpairmentRecovery = await _loanImpairmentRecoveryRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetLoanImpairmentRecoveryForEditOutput {LoanImpairmentRecovery = ObjectMapper.Map<CreateOrEditLoanImpairmentRecoveryDto>(loanImpairmentRecovery)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditLoanImpairmentRecoveryDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentRecoveries_Create)]
		 protected virtual async Task Create(CreateOrEditLoanImpairmentRecoveryDto input)
         {
            var loanImpairmentRecovery = ObjectMapper.Map<LoanImpairmentRecovery>(input);

			

            await _loanImpairmentRecoveryRepository.InsertAsync(loanImpairmentRecovery);
         }

		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentRecoveries_Edit)]
		 protected virtual async Task Update(CreateOrEditLoanImpairmentRecoveryDto input)
         {
            var loanImpairmentRecovery = await _loanImpairmentRecoveryRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, loanImpairmentRecovery);
         }

		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentRecoveries_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _loanImpairmentRecoveryRepository.DeleteAsync(input.Id);
         } 
    }
}