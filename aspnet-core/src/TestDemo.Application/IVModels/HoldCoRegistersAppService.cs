
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.IVModels.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.IVModels
{
	[AbpAuthorize(AppPermissions.Pages_HoldCoRegisters)]
    public class HoldCoRegistersAppService : TestDemoAppServiceBase, IHoldCoRegistersAppService
    {
		 private readonly IRepository<HoldCoRegister, Guid> _holdCoRegisterRepository;
		 

		  public HoldCoRegistersAppService(IRepository<HoldCoRegister, Guid> holdCoRegisterRepository ) 
		  {
			_holdCoRegisterRepository = holdCoRegisterRepository;
			
		  }

		 public async Task<PagedResultDto<GetHoldCoRegisterForViewDto>> GetAll(GetAllHoldCoRegistersInput input)
         {
			var statusFilter = input.StatusFilter.HasValue
                        ? (CalibrationStatusEnum) input.StatusFilter
                        : default;			
					
			var filteredHoldCoRegisters = _holdCoRegisterRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.StatusFilter.HasValue && input.StatusFilter > -1, e => e.Status == statusFilter);

			var pagedAndFilteredHoldCoRegisters = filteredHoldCoRegisters
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var holdCoRegisters = from o in pagedAndFilteredHoldCoRegisters
                         select new GetHoldCoRegisterForViewDto() {
							HoldCoRegister = new HoldCoRegisterDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredHoldCoRegisters.CountAsync();

            return new PagedResultDto<GetHoldCoRegisterForViewDto>(
                totalCount,
                await holdCoRegisters.ToListAsync()
            );
         }
		 
		 public async Task<GetHoldCoRegisterForViewDto> GetHoldCoRegisterForView(Guid id)
         {
            var holdCoRegister = await _holdCoRegisterRepository.GetAsync(id);

            var output = new GetHoldCoRegisterForViewDto { HoldCoRegister = ObjectMapper.Map<HoldCoRegisterDto>(holdCoRegister) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_HoldCoRegisters_Edit)]
		 public async Task<GetHoldCoRegisterForEditOutput> GetHoldCoRegisterForEdit(EntityDto<Guid> input)
         {
            var holdCoRegister = await _holdCoRegisterRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetHoldCoRegisterForEditOutput {HoldCoRegister = ObjectMapper.Map<CreateOrEditHoldCoRegisterDto>(holdCoRegister)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditHoldCoRegisterDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_HoldCoRegisters_Create)]
		 protected virtual async Task Create(CreateOrEditHoldCoRegisterDto input)
         {
            var holdCoRegister = ObjectMapper.Map<HoldCoRegister>(input);

			

            await _holdCoRegisterRepository.InsertAsync(holdCoRegister);
         }

		 [AbpAuthorize(AppPermissions.Pages_HoldCoRegisters_Edit)]
		 protected virtual async Task Update(CreateOrEditHoldCoRegisterDto input)
         {
            var holdCoRegister = await _holdCoRegisterRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, holdCoRegister);
         }

		 [AbpAuthorize(AppPermissions.Pages_HoldCoRegisters_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _holdCoRegisterRepository.DeleteAsync(input.Id);
         } 
    }
}