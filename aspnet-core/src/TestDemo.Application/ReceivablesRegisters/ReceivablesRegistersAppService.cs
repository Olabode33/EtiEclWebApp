
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
using TestDemo.ReceivablesCurrentPeriodDates.Dtos;
using TestDemo.ReceivablesForecasts.Dtos;
using TestDemo.ReceivablesInputs.Dtos;
using TestDemo.ReceivablesApprovals.Dtos;
using TestDemo.ReceivablesApprovals;
using TestDemo.Authorization.Users;

namespace TestDemo.ReceivablesRegisters
{
	[AbpAuthorize(AppPermissions.Pages_ReceivablesRegisters)]
    public class ReceivablesRegistersAppService : TestDemoAppServiceBase, IReceivablesRegistersAppService
    {
		private readonly IRepository<ReceivablesRegister, Guid> _receivablesRegisterRepository;
        private readonly IRepository<CurrentPeriodDate, Guid> _currentPeriodDateRepository;
        private readonly IRepository<ReceivablesForecast, Guid> _receivablesForecastRepository;
        private readonly IRepository<ReceivablesInput, Guid> _receivablesInputRepository;
        private readonly IRepository<ReceivablesApproval, Guid> _receivablesApprovalRepository;
        private readonly IRepository<User, long> _lookup_userRepository;


        public ReceivablesRegistersAppService(IRepository<ReceivablesRegister, Guid> receivablesRegisterRepository, IRepository<CurrentPeriodDate, Guid> currentPeriodDateRepository,
            IRepository<ReceivablesForecast, Guid> receivablesForecastRepository, IRepository<ReceivablesInput, Guid> receivablesInputRepository, IRepository<ReceivablesApproval, Guid> receivablesApprovalRepository, IRepository<User, long> lookup_userRepository) 
		  {
			_receivablesRegisterRepository = receivablesRegisterRepository;
            _currentPeriodDateRepository = currentPeriodDateRepository;
            _receivablesForecastRepository = receivablesForecastRepository;
            _receivablesInputRepository = receivablesInputRepository;
            _receivablesApprovalRepository = receivablesApprovalRepository;
            _lookup_userRepository = lookup_userRepository;
          }

        public async Task<PagedResultDto<GetReceivablesRegisterForViewDto>> GetAll(GetAllReceivablesRegistersInput input)
        {
            var statusFilter = input.StatusFilter.HasValue
                        ? (CalibrationStatusEnum)input.StatusFilter
                        : default;

            var filteredReceivablesRegisters = _receivablesRegisterRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.StatusFilter.HasValue && input.StatusFilter > -1, e => e.Status == statusFilter);

            var pagedAndFilteredReceivablesRegisters = filteredReceivablesRegisters
                .OrderBy(input.Sorting ?? "creationTime desc")
                .PageBy(input);

            var receivablesRegisters = from o in pagedAndFilteredReceivablesRegisters
                                       join o1 in _lookup_userRepository.GetAll() on o.CreatorUserId equals o1.Id into j1
                                       from s1 in j1.DefaultIfEmpty()

                                       select new GetReceivablesRegisterForViewDto()
                                       {
                                           ReceivablesRegister = new ReceivablesRegisterDto
                                           {
                                               Status = o.Status,
                                               Id = o.Id
                                           },

                                           DateCreated = o.CreationTime,
                                           CreatedBy = s1 == null ? "" : s1.FullName,
                                       };

            var totalCount = await filteredReceivablesRegisters.CountAsync();

            return new PagedResultDto<GetReceivablesRegisterForViewDto>(
                totalCount,
                await receivablesRegisters.ToListAsync()
            );
        }

        public async Task<GetReceivablesRegisterForViewDto> GetReceivablesRegisterForView(Guid id)
        {
            var receivablesRegister = await _receivablesRegisterRepository.GetAsync(id);

            var output = new GetReceivablesRegisterForViewDto { ReceivablesRegister = ObjectMapper.Map<ReceivablesRegisterDto>(receivablesRegister) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ReceivablesRegisters_Edit)]
		 public async Task<GetReceivablesRegisterForEditOutput> GetReceivablesRegisterForEdit(EntityDto<Guid> input)
         {
         
            var receivablesRegister = await _receivablesRegisterRepository.FirstOrDefaultAsync(input.Id);
            var cpd = await _currentPeriodDateRepository.GetAll().Where(a => a.RegisterId == input.Id).ToListAsync();
            var inputParam = await _receivablesInputRepository.SingleAsync(a => a.RegisterId == input.Id);
            var forecast = await _receivablesForecastRepository.GetAll().Where(a => a.RegisterId == input.Id).ToListAsync();

            var output = new GetReceivablesRegisterForEditOutput { ReceivablesRegister = ObjectMapper.Map<CreateOrEditReceivablesRegisterDto>(receivablesRegister) };
            output.ReceivablesRegister.CurrentPeriodData = ObjectMapper.Map<List<CreateOrEditCurrentPeriodDateDto>>(cpd);
            output.ReceivablesRegister.InputParameter = ObjectMapper.Map<CreateOrEditReceivablesInputDto>(inputParam);
            output.ReceivablesRegister.ForecastData = ObjectMapper.Map<List<CreateOrEditReceivablesForecastDto>>(forecast);

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
            input.Status = CalibrationStatusEnum.Submitted;
            var receivablesRegister = ObjectMapper.Map<ReceivablesRegister>(input);

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

            await _receivablesApprovalRepository.InsertAsync(new ReceivablesApproval
            {
                RegisterId = registerId,
                ReviewComment = "",
                ReviewedByUserId = (long)AbpSession.UserId,
                ReviewedDate = DateTime.Now,
                Status = input.Status
            });
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

            await _currentPeriodDateRepository.DeleteAsync(a => a.RegisterId == input.Id);
            await _receivablesInputRepository.DeleteAsync(a => a.RegisterId == input.Id);
            await _receivablesForecastRepository.DeleteAsync(a => a.RegisterId == input.Id);
        }

        public async Task ApproveRejectModel(CreateOrEditReceivablesApprovalDto input)
        {
            var reg = await _receivablesRegisterRepository.FirstOrDefaultAsync(input.RegisterId);

            reg.Status = input.Status;
            await _receivablesRegisterRepository.UpdateAsync(reg);


            await _receivablesApprovalRepository.InsertAsync(new ReceivablesApproval
            {
                RegisterId = input.RegisterId,
                ReviewComment = input.ReviewComment,
                ReviewedByUserId = (long)AbpSession.UserId,
                ReviewedDate = DateTime.Now,
                Status = input.Status
            });
        }
    }
}