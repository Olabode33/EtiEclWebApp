

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ReceivablesInputs.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.ReceivablesInputs
{
	//[AbpAuthorize(AppPermissions.Pages_ReceivablesInputs)]
    public class ReceivablesInputsAppService : TestDemoAppServiceBase, IReceivablesInputsAppService
    {
		 private readonly IRepository<ReceivablesInput, Guid> _receivablesInputRepository;
		 

		  public ReceivablesInputsAppService(IRepository<ReceivablesInput, Guid> receivablesInputRepository ) 
		  {
			_receivablesInputRepository = receivablesInputRepository;
			
		  }

		 public async Task<PagedResultDto<GetReceivablesInputForViewDto>> GetAll(GetAllReceivablesInputsInput input)
         {
			
			var filteredReceivablesInputs = _receivablesInputRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false );

			var pagedAndFilteredReceivablesInputs = filteredReceivablesInputs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var receivablesInputs = from o in pagedAndFilteredReceivablesInputs
                         select new GetReceivablesInputForViewDto() {
							ReceivablesInput = new ReceivablesInputDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredReceivablesInputs.CountAsync();

            return new PagedResultDto<GetReceivablesInputForViewDto>(
                totalCount,
                await receivablesInputs.ToListAsync()
            );
         }
		 
		 public async Task<GetReceivablesInputForViewDto> GetReceivablesInputForView(Guid id)
         {
            var receivablesInput = await _receivablesInputRepository.GetAsync(id);

            var output = new GetReceivablesInputForViewDto { ReceivablesInput = ObjectMapper.Map<ReceivablesInputDto>(receivablesInput) };
			
            return output;
         }
		 
		 //[AbpAuthorize(AppPermissions.Pages_ReceivablesInputs_Edit)]
		 public async Task<GetReceivablesInputForEditOutput> GetReceivablesInputForEdit(EntityDto<Guid> input)
         {
            var receivablesInput = await _receivablesInputRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetReceivablesInputForEditOutput {ReceivablesInput = ObjectMapper.Map<CreateOrEditReceivablesInputDto>(receivablesInput)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditReceivablesInputDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 //[AbpAuthorize(AppPermissions.Pages_ReceivablesInputs_Create)]
		 protected virtual async Task Create(CreateOrEditReceivablesInputDto input)
         {
            var receivablesInput = ObjectMapper.Map<ReceivablesInput>(input);

			

            await _receivablesInputRepository.InsertAsync(receivablesInput);
         }

		 //[AbpAuthorize(AppPermissions.Pages_ReceivablesInputs_Edit)]
		 protected virtual async Task Update(CreateOrEditReceivablesInputDto input)
         {
            var receivablesInput = await _receivablesInputRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, receivablesInput);
         }

		 //[AbpAuthorize(AppPermissions.Pages_ReceivablesInputs_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _receivablesInputRepository.DeleteAsync(input.Id);
         } 
    }
}