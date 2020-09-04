
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ReceivablesRegisters.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.ReceivablesCurrentPeriodDates;
using TestDemo.ReceivablesForecasts;
using TestDemo.ReceivablesInputs;

namespace TestDemo.ReceivablesRegisters
{
	[AbpAuthorize(AppPermissions.Pages_ReceivablesRegisters)]
    public class ReceivablesRegistersAppService : TestDemoAppServiceBase, IReceivablesRegistersAppService
    {
		private readonly IRepository<ReceivablesRegister, Guid> _receivablesRegisterRepository;
        private readonly IRepository<CurrentPeriodDate, Guid> _currentPeriodDateRepository;
        private readonly IRepository<ReceivablesForecast, Guid> _receivablesForecastRepository;
        private readonly IRepository<ReceivablesInput, Guid> _receivablesInputRepository;


        public ReceivablesRegistersAppService(IRepository<ReceivablesRegister, Guid> receivablesRegisterRepository ) 
		  {
			_receivablesRegisterRepository = receivablesRegisterRepository;
			
		  }

		 public async Task<PagedResultDto<GetReceivablesRegisterForViewDto>> GetAll(GetAllReceivablesRegistersInput input)
         {
			var statusFilter = input.StatusFilter.HasValue
                        ? (CalibrationStatusEnum) input.StatusFilter
                        : default;			
					
			var filteredReceivablesRegisters = _receivablesRegisterRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.StatusFilter.HasValue && input.StatusFilter > -1, e => e.Status == statusFilter);

			var pagedAndFilteredReceivablesRegisters = filteredReceivablesRegisters
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var receivablesRegisters = from o in pagedAndFilteredReceivablesRegisters
                         select new GetReceivablesRegisterForViewDto() {
							ReceivablesRegister = new ReceivablesRegisterDto
							{
                                Status = o.Status,
                                Id = o.Id
							}
						};

            var totalCount = await filteredReceivablesRegisters.CountAsync();

            return new PagedResultDto<GetReceivablesRegisterForViewDto>(
                totalCount,
                await receivablesRegisters.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ReceivablesRegisters_Edit)]
		 public async Task<GetReceivablesRegisterForEditOutput> GetReceivablesRegisterForEdit(EntityDto<Guid> input)
         {
            var receivablesRegister = await _receivablesRegisterRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetReceivablesRegisterForEditOutput {ReceivablesRegister = ObjectMapper.Map<CreateOrEditReceivablesRegisterDto>(receivablesRegister)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditReceivablesRegisterDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ReceivablesRegisters_Create)]
		 protected virtual async Task Create(CreateOrEditReceivablesRegisterDto input)
         {
            var receivablesRegister = ObjectMapper.Map<ReceivablesRegister>(input);

            input.Status = CalibrationStatusEnum.Submitted;

            var registerId = await _receivablesRegisterRepository.InsertAndGetIdAsync(receivablesRegister);

            foreach (var cpd in input.CurrentPeriodData)
            {
                cpd.RegisterId = registerId;
                var ab = ObjectMapper.Map<CurrentPeriodDate>(cpd);
                _currentPeriodDateRepository.Insert(ab);

            }

            foreach (var fd in input.ForecastData)
            {
                fd.RegisterId = registerId;
                var macECI = ObjectMapper.Map<ReceivablesForecast>(fd);
                _receivablesForecastRepository.Insert(macECI);

            }

            input.InputParameter.RegisterId = registerId;
            var inp = ObjectMapper.Map<ReceivablesInput>(input.InputParameter);
            _receivablesInputRepository.Insert(inp);

            //await _holdCoApprovalRepository.InsertAsync(new HoldCoApproval
            //{
            //    RegistrationId = registrationId,
            //    ReviewComment = "",
            //    ReviewedByUserId = (long)AbpSession.UserId,
            //    ReviewedDate = DateTime.Now,
            //    Status = input.Status
            //});
        }

		 [AbpAuthorize(AppPermissions.Pages_ReceivablesRegisters_Edit)]
		 protected virtual async Task Update(CreateOrEditReceivablesRegisterDto input)
         {
            var receivablesRegister = await _receivablesRegisterRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, receivablesRegister);
         }

		 [AbpAuthorize(AppPermissions.Pages_ReceivablesRegisters_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _receivablesRegisterRepository.DeleteAsync(input.Id);
         } 
    }
}