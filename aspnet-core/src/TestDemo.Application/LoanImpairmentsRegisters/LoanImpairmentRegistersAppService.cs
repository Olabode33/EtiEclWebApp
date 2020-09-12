
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.LoanImpairmentsRegisters.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.LoanImpairmentKeyParameters;
using TestDemo.LoanImpairmentRecoveries;
using TestDemo.LoanImpairmentInputParameters;
using TestDemo.LoanImpairmentHaircuts;
using TestDemo.LoanImpairmentScenarios;
using TestDemo.LoanImpairmentKeyParameters.Dtos;
using TestDemo.LoanImpairmentHaircuts.Dtos;
using TestDemo.LoanImpairmentInputParameters.Dtos;
using TestDemo.LoanImpairmentRecoveries.Dtos;
using TestDemo.LoanImpairmentScenarios.Dtos;
using TestDemo.LoanImpairmentApprovals.Dtos;
using TestDemo.LoanImpairmentApprovals;
using TestDemo.Authorization.Users;

namespace TestDemo.LoanImpairmentsRegisters
{
	[AbpAuthorize(AppPermissions.Pages_LoanImpairmentRegisters)]
    public class LoanImpairmentRegistersAppService : TestDemoAppServiceBase, ILoanImpairmentRegistersAppService
    {
		private readonly IRepository<LoanImpairmentRegister, Guid> _loanImpairmentRegisterRepository;
        private readonly IRepository<LoanImpairmentKeyParameter, Guid> _loanImpairmentKeyParameterRepository;
        private readonly IRepository<LoanImpairmentRecovery, Guid> _loanImpairmentRecoveryRepository;
        private readonly IRepository<LoanImpairmentScenario, Guid> _loanImpairmentScenarioRepository;
        private readonly IRepository<LoanImpairmentInputParameter, Guid> _loanImpairmentInputParameterRepository;
        private readonly IRepository<LoanImpairmentHaircut, Guid> _loanImpairmentHaircutRepository;
        private readonly IRepository<LoanImpairmentApproval, Guid> _loanImpairmentApprovalRepository;
        private readonly IRepository<User, long> _lookup_userRepository;


        public LoanImpairmentRegistersAppService(IRepository<LoanImpairmentRegister, Guid> loanImpairmentRegisterRepository, IRepository<LoanImpairmentKeyParameter, Guid> loanImpairmentKeyParameterRepository,
            IRepository<LoanImpairmentRecovery, Guid> loanImpairmentRecoveryRepository, IRepository<LoanImpairmentScenario, Guid> loanImpairmentScenarioRepository,
            IRepository<LoanImpairmentInputParameter, Guid> loanImpairmentInputParameterRepository, IRepository<LoanImpairmentHaircut, Guid> loanImpairmentHaircutRepository, IRepository<User, long> lookup_userRepository,
            IRepository<LoanImpairmentApproval, Guid> loanImpairmentApprovalRepository) 
		  {
			_loanImpairmentRegisterRepository = loanImpairmentRegisterRepository;
            _loanImpairmentKeyParameterRepository = loanImpairmentKeyParameterRepository;
            _loanImpairmentRecoveryRepository = loanImpairmentRecoveryRepository;
            _loanImpairmentScenarioRepository = loanImpairmentScenarioRepository;
            _loanImpairmentInputParameterRepository = loanImpairmentInputParameterRepository;
            _loanImpairmentHaircutRepository = loanImpairmentHaircutRepository;
            _loanImpairmentApprovalRepository = loanImpairmentApprovalRepository;
            _lookup_userRepository = lookup_userRepository;
          }

		 public async Task<PagedResultDto<GetLoanImpairmentRegisterForViewDto>> GetAll(GetAllLoanImpairmentRegistersInput input)
         {
			var statusFilter = input.StatusFilter.HasValue
                        ? (CalibrationStatusEnum) input.StatusFilter
                        : default;			
					
			var filteredLoanImpairmentRegisters = _loanImpairmentRegisterRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.StatusFilter.HasValue && input.StatusFilter > -1, e => e.Status == statusFilter);

			var pagedAndFilteredLoanImpairmentRegisters = filteredLoanImpairmentRegisters
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var loanImpairmentRegisters = from o in pagedAndFilteredLoanImpairmentRegisters
                                          join o1 in _lookup_userRepository.GetAll() on o.CreatorUserId equals o1.Id into j1
                                          from s1 in j1.DefaultIfEmpty()

                                          select new GetLoanImpairmentRegisterForViewDto() {
							LoanImpairmentRegister = new LoanImpairmentRegisterDto
							{
                                Status = o.Status,
                                Id = o.Id
							},
                             DateCreated = o.CreationTime,
                             CreatedBy = s1 == null ? "" : s1.FullName,
                         };

            var totalCount = await filteredLoanImpairmentRegisters.CountAsync();

            return new PagedResultDto<GetLoanImpairmentRegisterForViewDto>(
                totalCount,
                await loanImpairmentRegisters.ToListAsync()
            );
         }
		 
		 public async Task<GetLoanImpairmentRegisterForViewDto> GetLoanImpairmentRegisterForView(Guid id)
         {
            var loanImpairmentRegister = await _loanImpairmentRegisterRepository.GetAsync(id);

            var output = new GetLoanImpairmentRegisterForViewDto { LoanImpairmentRegister = ObjectMapper.Map<LoanImpairmentRegisterDto>(loanImpairmentRegister) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentRegisters_Edit)]
		 public async Task<GetLoanImpairmentRegisterForEditOutput> GetLoanImpairmentRegisterForEdit(EntityDto<Guid> input)
         {
            var loanImpairmentRegister = await _loanImpairmentRegisterRepository.FirstOrDefaultAsync(input.Id);
           

            var likp = await _loanImpairmentKeyParameterRepository.GetAll().Where(a => a.RegisterId == input.Id).ToListAsync();
            var lir = await _loanImpairmentRecoveryRepository.GetAll().Where(a => a.RegisterId == input.Id).ToListAsync();
            var lis = await _loanImpairmentScenarioRepository.GetAll().Where(a => a.RegisterId == input.Id).ToListAsync();
            var liip = await _loanImpairmentInputParameterRepository.SingleAsync(a => a.RegisterId == input.Id);
            var lih = await _loanImpairmentHaircutRepository.SingleAsync(a => a.RegisterId == input.Id);

            var output = new GetLoanImpairmentRegisterForEditOutput { LoanImpairmentRegister = ObjectMapper.Map<CreateOrEditLoanImpairmentRegisterDto>(loanImpairmentRegister) };
            output.LoanImpairmentRegister.CalibrationOfKeyParameters = ObjectMapper.Map<List<CreateOrEditLoanImpairmentKeyParameterDto>>(likp);
            output.LoanImpairmentRegister.HaircutRecovery = ObjectMapper.Map<CreateOrEditLoanImpairmentHaircutDto>(lih);
            output.LoanImpairmentRegister.InputParameter = ObjectMapper.Map<CreateOrEditLoanImpairmentInputParameterDto>(liip);
            output.LoanImpairmentRegister.LoanImpairmentRecovery = ObjectMapper.Map<List<CreateOrEditLoanImpairmentRecoveryDto>>(lir);
            output.LoanImpairmentRegister.LoanImpairmentScenarios = ObjectMapper.Map<List<CreateOrEditLoanImpairmentScenarioDto>>(lis);

            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditLoanImpairmentRegisterDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentRegisters_Create)]
		 protected virtual async Task Create(CreateOrEditLoanImpairmentRegisterDto input)
         {
            input.Status = CalibrationStatusEnum.Submitted;
            var loanImpairmentRegister = ObjectMapper.Map<LoanImpairmentRegister>(input);

            var registerId = await _loanImpairmentRegisterRepository.InsertAndGetIdAsync(loanImpairmentRegister);

            foreach (var ckp in input.CalibrationOfKeyParameters)
            {
                ckp.RegisterId = registerId;
                var ab = ObjectMapper.Map<LoanImpairmentKeyParameter>(ckp);
                _loanImpairmentKeyParameterRepository.Insert(ab);
            }

            foreach (var item in input.LoanImpairmentRecovery)
            {
                item.RegisterId = registerId;
                var lir = ObjectMapper.Map<LoanImpairmentRecovery>(item);
                _loanImpairmentRecoveryRepository.Insert(lir);
            }

            foreach (var item in input.LoanImpairmentScenarios)
            {
                item.RegisterId = registerId;
                var lis = ObjectMapper.Map<LoanImpairmentScenario>(item);
                _loanImpairmentScenarioRepository.Insert(lis);
            }

            input.InputParameter.RegisterId = registerId;
            var inp = ObjectMapper.Map<LoanImpairmentInputParameter>(input.InputParameter);
            _loanImpairmentInputParameterRepository.Insert(inp);

            input.HaircutRecovery.RegisterId = registerId;
            var hc = ObjectMapper.Map<LoanImpairmentHaircut>(input.HaircutRecovery);
            _loanImpairmentHaircutRepository.Insert(hc);

            await _loanImpairmentApprovalRepository.InsertAsync(new LoanImpairmentApproval
            {
                RegisterId = registerId,
                ReviewComment = "",
                ReviewedByUserId = (long)AbpSession.UserId,
                ReviewedDate = DateTime.Now,
                Status = input.Status
            });
        }

		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentRegisters_Edit)]
		 protected virtual async Task Update(CreateOrEditLoanImpairmentRegisterDto input)
         {
            var loanImpairmentRegister = await _loanImpairmentRegisterRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, loanImpairmentRegister);
         }

		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentRegisters_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _loanImpairmentRegisterRepository.DeleteAsync(input.Id);

            await _loanImpairmentKeyParameterRepository.DeleteAsync(a => a.RegisterId == input.Id);
            await _loanImpairmentRecoveryRepository.DeleteAsync(a => a.RegisterId == input.Id);
            await _loanImpairmentScenarioRepository.DeleteAsync(a => a.RegisterId == input.Id);
            await _loanImpairmentInputParameterRepository.DeleteAsync(a => a.RegisterId == input.Id);
            await _loanImpairmentHaircutRepository.DeleteAsync(a => a.RegisterId == input.Id);
        }

        public async Task Rerun(CreateOrEditLoanImpairmentRegisterDto input)
        {
            input.Status = CalibrationStatusEnum.QueuedForRerun;
            var loanImpairmentRegister = await _loanImpairmentRegisterRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, loanImpairmentRegister);
        }

            public async Task ApproveRejectModel(CreateOrEditLoanImpairmentApprovalDto input)
        {
            var reg = await _loanImpairmentRegisterRepository.FirstOrDefaultAsync(input.RegisterId);

            reg.Status = input.Status;
            await _loanImpairmentRegisterRepository.UpdateAsync(reg);


            await _loanImpairmentApprovalRepository.InsertAsync(new LoanImpairmentApproval
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