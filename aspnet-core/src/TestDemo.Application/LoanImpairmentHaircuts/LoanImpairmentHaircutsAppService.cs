

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.LoanImpairmentHaircuts.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.LoanImpairmentHaircuts
{
	//[AbpAuthorize(AppPermissions.Pages_LoanImpairmentHaircuts)]
    public class LoanImpairmentHaircutsAppService : TestDemoAppServiceBase, ILoanImpairmentHaircutsAppService
    {
		 private readonly IRepository<LoanImpairmentHaircut, Guid> _loanImpairmentHaircutRepository;
		 

		  public LoanImpairmentHaircutsAppService(IRepository<LoanImpairmentHaircut, Guid> loanImpairmentHaircutRepository ) 
		  {
			_loanImpairmentHaircutRepository = loanImpairmentHaircutRepository;
			
		  }

		 public async Task<PagedResultDto<GetLoanImpairmentHaircutForViewDto>> GetAll(GetAllLoanImpairmentHaircutsInput input)
         {
			
			var filteredLoanImpairmentHaircuts = _loanImpairmentHaircutRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false );

			var pagedAndFilteredLoanImpairmentHaircuts = filteredLoanImpairmentHaircuts
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var loanImpairmentHaircuts = from o in pagedAndFilteredLoanImpairmentHaircuts
                         select new GetLoanImpairmentHaircutForViewDto() {
							LoanImpairmentHaircut = new LoanImpairmentHaircutDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredLoanImpairmentHaircuts.CountAsync();

            return new PagedResultDto<GetLoanImpairmentHaircutForViewDto>(
                totalCount,
                await loanImpairmentHaircuts.ToListAsync()
            );
         }
		 
		 public async Task<GetLoanImpairmentHaircutForViewDto> GetLoanImpairmentHaircutForView(Guid id)
         {
            var loanImpairmentHaircut = await _loanImpairmentHaircutRepository.GetAsync(id);

            var output = new GetLoanImpairmentHaircutForViewDto { LoanImpairmentHaircut = ObjectMapper.Map<LoanImpairmentHaircutDto>(loanImpairmentHaircut) };
			
            return output;
         }
		 
		 //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentHaircuts_Edit)]
		 public async Task<GetLoanImpairmentHaircutForEditOutput> GetLoanImpairmentHaircutForEdit(EntityDto<Guid> input)
         {
            var loanImpairmentHaircut = await _loanImpairmentHaircutRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetLoanImpairmentHaircutForEditOutput {LoanImpairmentHaircut = ObjectMapper.Map<CreateOrEditLoanImpairmentHaircutDto>(loanImpairmentHaircut)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditLoanImpairmentHaircutDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentHaircuts_Create)]
		 protected virtual async Task Create(CreateOrEditLoanImpairmentHaircutDto input)
         {
            var loanImpairmentHaircut = ObjectMapper.Map<LoanImpairmentHaircut>(input);

			

            await _loanImpairmentHaircutRepository.InsertAsync(loanImpairmentHaircut);
         }

		 //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentHaircuts_Edit)]
		 protected virtual async Task Update(CreateOrEditLoanImpairmentHaircutDto input)
         {
            var loanImpairmentHaircut = await _loanImpairmentHaircutRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, loanImpairmentHaircut);
         }

		 //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentHaircuts_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _loanImpairmentHaircutRepository.DeleteAsync(input.Id);
         } 
    }
}