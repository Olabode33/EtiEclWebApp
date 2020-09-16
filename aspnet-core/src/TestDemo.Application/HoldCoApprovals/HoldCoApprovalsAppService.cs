
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.HoldCoApprovals.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.Authorization.Users;
using TestDemo.IVModels;

namespace TestDemo.HoldCoApprovals
{
	//[AbpAuthorize(AppPermissions.Pages_HoldCoApprovals)]
    public class HoldCoApprovalsAppService : TestDemoAppServiceBase, IHoldCoApprovalsAppService
    {
		private readonly IRepository<HoldCoApproval> _holdCoApprovalRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<HoldCoRegister, Guid> _holdCoRegisterRepository;


        public HoldCoApprovalsAppService(IRepository<HoldCoApproval> holdCoApprovalRepository, IRepository<User, long> lookup_userRepository, IRepository<HoldCoRegister, Guid> holdCoRegisterRepository)
        {
			_holdCoApprovalRepository = holdCoApprovalRepository;
            _lookup_userRepository = lookup_userRepository;
            _holdCoRegisterRepository = holdCoRegisterRepository;

          }

		 public async Task<PagedResultDto<GetHoldCoApprovalForViewDto>> GetAll(GetAllHoldCoApprovalsInput input)
         {
			
			var filteredHoldCoApprovals = _holdCoApprovalRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ReviewComment.Contains(input.Filter));

			var pagedAndFilteredHoldCoApprovals = filteredHoldCoApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var holdCoApprovals = from o in pagedAndFilteredHoldCoApprovals
                         select new GetHoldCoApprovalForViewDto() {
							HoldCoApproval = new HoldCoApprovalDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredHoldCoApprovals.CountAsync();

            return new PagedResultDto<GetHoldCoApprovalForViewDto>(
                totalCount,
                await holdCoApprovals.ToListAsync()
            );
         }

        public async Task<HoldCoAuditInfoDto> GetHoldCoApprovals(Guid id)
        {
            var holdCoApprovals = await _holdCoApprovalRepository.GetAll().Where(a => a.RegistrationId == id).ToListAsync();

            var register = _holdCoRegisterRepository.FirstOrDefault(id);

            var approvals = from o in holdCoApprovals
                            join o1 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o1.Id into j1
                            from s1 in j1.DefaultIfEmpty()

                            select new HoldCoApprovalAuditInfoDto()
                            {
                                RegistrationId = id,
                                ReviewedDate = o.ReviewedDate,
                                Status = o.Status,
                                ReviewComment = o.ReviewComment,
                                ReviewedBy = s1 == null ? "" : s1.FullName.ToString()
                            };

            var auditInfo = new HoldCoAuditInfoDto
            {
                DateCreated = register.CreationTime,
                LastUpdated = register.LastModificationTime,
                UpdatedBy = register.LastModifierUserId == null ? "" : _lookup_userRepository.FirstOrDefault((long)register.LastModifierUserId)?.FullName,
                Approvals = approvals.ToList(),
                CreatedBy = _lookup_userRepository.FirstOrDefault((long)register.CreatorUserId)?.FullName
            };

            return auditInfo;
        }

        public async Task<GetHoldCoApprovalForViewDto> GetHoldCoApprovalForView(int id)
         {
            var holdCoApproval = await _holdCoApprovalRepository.GetAsync(id);

            var output = new GetHoldCoApprovalForViewDto { HoldCoApproval = ObjectMapper.Map<HoldCoApprovalDto>(holdCoApproval) };
			
            return output;
         }
		 
		 //[AbpAuthorize(AppPermissions.Pages_HoldCoApprovals_Edit)]
		 public async Task<GetHoldCoApprovalForEditOutput> GetHoldCoApprovalForEdit(EntityDto input)
         {
            var holdCoApproval = await _holdCoApprovalRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetHoldCoApprovalForEditOutput {HoldCoApproval = ObjectMapper.Map<CreateOrEditHoldCoApprovalDto>(holdCoApproval)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditHoldCoApprovalDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 //[AbpAuthorize(AppPermissions.Pages_HoldCoApprovals_Create)]
		 protected virtual async Task Create(CreateOrEditHoldCoApprovalDto input)
         {
            var holdCoApproval = ObjectMapper.Map<HoldCoApproval>(input);

			

            await _holdCoApprovalRepository.InsertAsync(holdCoApproval);
         }

		 //[AbpAuthorize(AppPermissions.Pages_HoldCoApprovals_Edit)]
		 protected virtual async Task Update(CreateOrEditHoldCoApprovalDto input)
         {
            var holdCoApproval = await _holdCoApprovalRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, holdCoApproval);
         }

		 //[AbpAuthorize(AppPermissions.Pages_HoldCoApprovals_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _holdCoApprovalRepository.DeleteAsync(input.Id);
         } 
    }
}