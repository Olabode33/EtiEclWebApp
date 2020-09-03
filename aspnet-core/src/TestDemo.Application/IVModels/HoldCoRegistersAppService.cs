
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
using TestDemo.Authorization.Users;
using TestDemo.HoldCoAssetBook;
using TestDemo.HoldCoAssetBook.Dtos;
using TestDemo.HoldCoApprovals;

namespace TestDemo.IVModels
{
	[AbpAuthorize(AppPermissions.Pages_HoldCoRegisters)]
    public class HoldCoRegistersAppService : TestDemoAppServiceBase, IHoldCoRegistersAppService
    {
        private readonly IRepository<HoldCoRegister, Guid> _holdCoRegisterRepository;
        private readonly IRepository<AssetBook, Guid> _assetBookRepository;
        private readonly IRepository<MacroEconomicCreditIndex, Guid> _macroEconomicCreditIndexRepository;
        private readonly IRepository<HoldCoInputParameter, Guid> _holdCoInputParameterRepository;
        private readonly IRepository<HoldCoApproval> _holdCoApprovalRepository;
        private readonly IRepository<User, long> _lookup_userRepository;

        public HoldCoRegistersAppService(IRepository<HoldCoRegister, Guid> holdCoRegisterRepository, IRepository<AssetBook, Guid> assetBookRepository, IRepository<MacroEconomicCreditIndex, Guid> macroEconomicCreditIndexRepository, IRepository<HoldCoInputParameter, Guid> holdCoInputParameterRepository, IRepository<User, long> lookup_userRepository, IRepository<HoldCoApproval> holdCoApprovalRepository)
        {
            _holdCoRegisterRepository = holdCoRegisterRepository;
            _assetBookRepository = assetBookRepository;
            _macroEconomicCreditIndexRepository = macroEconomicCreditIndexRepository;
            _holdCoInputParameterRepository = holdCoInputParameterRepository;
            _lookup_userRepository = lookup_userRepository;
            _holdCoApprovalRepository = holdCoApprovalRepository;
        }

        public async Task<PagedResultDto<GetHoldCoRegisterForViewDto>> GetAll(GetAllHoldCoRegistersInput input)
        {
            var statusFilter = input.StatusFilter.HasValue
                        ? (CalibrationStatusEnum)input.StatusFilter
                        : default;

            var filteredHoldCoRegisters = _holdCoRegisterRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.StatusFilter.HasValue && input.StatusFilter > -1, e => e.Status == statusFilter);

            var pagedAndFilteredHoldCoRegisters = filteredHoldCoRegisters
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var holdCoRegisters = from o in pagedAndFilteredHoldCoRegisters
                                  join o1 in _lookup_userRepository.GetAll() on o.CreatorUserId equals o1.Id into j1
                                  from s1 in j1.DefaultIfEmpty()

                                  select new GetHoldCoRegisterForViewDto()
                                  {
                                      HoldCoRegister = new HoldCoRegisterDto
                                      {
                                          Status = o.Status,
                                          Id = o.Id
                                      },
                                      DateCreated = o.CreationTime,
                                      CreatedBy = s1 == null ? "" : s1.FullName,
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
            var assetBooks = await _assetBookRepository.GetAll().Where(a => a.RegistrationId == input.Id).ToListAsync();
            var inputParam = await _holdCoInputParameterRepository.SingleAsync(a => a.RegistrationId == input.Id);
            var macros = await _macroEconomicCreditIndexRepository.GetAll().Where(a => a.RegistrationId == input.Id).ToListAsync();


            var output = new GetHoldCoRegisterForEditOutput {HoldCoRegister = ObjectMapper.Map<CreateOrEditHoldCoRegisterDto>(holdCoRegister)};
            output.HoldCoRegister.AssetBook = ObjectMapper.Map<List<CreateOrEditAssetBookDto>>(assetBooks);
            output.HoldCoRegister.InputParameter = ObjectMapper.Map<CreateOrEditHoldCoInputParameterDto>(inputParam);
            output.HoldCoRegister.MacroEconomicCreditIndex = ObjectMapper.Map<List<CreateOrEditMacroEconomicCreditIndexDto>>(macros);


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
            input.Status = CalibrationStatusEnum.Submitted;
            var holdCoRegister = ObjectMapper.Map<HoldCoRegister>(input);

            var registrationId = await _holdCoRegisterRepository.InsertAndGetIdAsync(holdCoRegister);

            foreach (var abook in input.AssetBook)
            {
                abook.RegistrationId = registrationId;
                var ab = ObjectMapper.Map<AssetBook>(abook);
                _assetBookRepository.Insert(ab);

            }

            foreach (var meci in input.MacroEconomicCreditIndex)
            {
                meci.RegistrationId = registrationId;
                var macECI = ObjectMapper.Map<MacroEconomicCreditIndex>(meci);
                _macroEconomicCreditIndexRepository.Insert(macECI);

            }

            input.InputParameter.RegistrationId = registrationId;
            var inp = ObjectMapper.Map<HoldCoInputParameter>(input.InputParameter);
            _holdCoInputParameterRepository.Insert(inp);

            await _holdCoApprovalRepository.InsertAsync(new HoldCoApproval
            {
                RegistrationId = registrationId,
                ReviewComment = "",
                ReviewedByUserId = (long)AbpSession.UserId,
                ReviewedDate = DateTime.Now,
                Status = input.Status
            });
        }

        [AbpAuthorize(AppPermissions.Pages_HoldCoRegisters_Edit)]
		 protected virtual async Task Update(CreateOrEditHoldCoRegisterDto input)
         {
            var holdCoRegister = await _holdCoRegisterRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, holdCoRegister);

            foreach (var abook in input.AssetBook)
            {
                var ab = ObjectMapper.Map<AssetBook>(abook);
               await  _assetBookRepository.UpdateAsync(ab);

            }

            foreach (var meci in input.MacroEconomicCreditIndex)
            {
                var macECI = ObjectMapper.Map<MacroEconomicCreditIndex>(meci);
              await  _macroEconomicCreditIndexRepository.UpdateAsync(macECI);

            }

            var inp = ObjectMapper.Map<HoldCoInputParameter>(input.InputParameter);
            await _holdCoInputParameterRepository.UpdateAsync(inp);
        }

		 [AbpAuthorize(AppPermissions.Pages_HoldCoRegisters_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _holdCoRegisterRepository.DeleteAsync(input.Id);
            await _assetBookRepository.DeleteAsync(a => a.RegistrationId == input.Id);
            await _macroEconomicCreditIndexRepository.DeleteAsync(a => a.RegistrationId == input.Id);
            await _holdCoInputParameterRepository.DeleteAsync(a => a.RegistrationId == input.Id);
        }

        public async Task ApproveRejectModel(CreateOrEditHoldCoRegisterApprovalDto input)
        {
            var reg = await _holdCoRegisterRepository.FirstOrDefaultAsync(input.RegistrationId);

            reg.Status = input.Status;
            await _holdCoRegisterRepository.UpdateAsync(reg);


            await _holdCoApprovalRepository.InsertAsync(new HoldCoApproval
            {
                RegistrationId = input.RegistrationId,
                ReviewComment = input.ReviewComment,
                ReviewedByUserId = (long)AbpSession.UserId,
                ReviewedDate = DateTime.Now,
                Status = input.Status
            });
        }
    }
}