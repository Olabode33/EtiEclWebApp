
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ReceivablesApprovals.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.ReceivablesRegisters;
using TestDemo.Authorization.Users;

namespace TestDemo.ReceivablesApprovals
{
	//[AbpAuthorize(AppPermissions.Pages_ReceivablesApprovals)]
    public class ReceivablesApprovalsAppService : TestDemoAppServiceBase, IReceivablesApprovalsAppService
    {
		private readonly IRepository<ReceivablesApproval, Guid> _receivablesApprovalRepository;
        private readonly IRepository<ReceivablesRegister, Guid> _receivablesRegisterRepository;
        private readonly IRepository<User, long> _lookup_userRepository;


        public ReceivablesApprovalsAppService(IRepository<ReceivablesApproval, Guid> receivablesApprovalRepository, IRepository<User, long> lookup_userRepository, IRepository<ReceivablesRegister, Guid> receivablesRegisterRepository) 
		  {
			_receivablesApprovalRepository = receivablesApprovalRepository;
            _lookup_userRepository = lookup_userRepository;
            _receivablesRegisterRepository = receivablesRegisterRepository;
          }

		 public async Task<PagedResultDto<GetReceivablesApprovalForViewDto>> GetAll(GetAllReceivablesApprovalsInput input)
         {
			
			var filteredReceivablesApprovals = _receivablesApprovalRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ReviewComment.Contains(input.Filter));

			var pagedAndFilteredReceivablesApprovals = filteredReceivablesApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var receivablesApprovals = from o in pagedAndFilteredReceivablesApprovals
                         select new GetReceivablesApprovalForViewDto() {
							ReceivablesApproval = new ReceivablesApprovalDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredReceivablesApprovals.CountAsync();

            return new PagedResultDto<GetReceivablesApprovalForViewDto>(
                totalCount,
                await receivablesApprovals.ToListAsync()
            );
         }

        public async Task<ReceivablesAuditInfoDto> GetReceivablesApprovals(Guid id)
        {
            var receivablesApprovals = await _receivablesApprovalRepository.GetAll().Where(a => a.RegisterId == id).ToListAsync();

            var register = _receivablesRegisterRepository.FirstOrDefault(id);

            var approvals = from o in receivablesApprovals
                            join o1 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o1.Id into j1
                            from s1 in j1.DefaultIfEmpty()

                            select new ReceivablesApprovalAuditInfoDto()
                            {
                                RegisterId = id,
                                ReviewedDate = o.ReviewedDate,
                                Status = o.Status,
                                ReviewComment = o.ReviewComment,
                                ReviewedBy = s1 == null ? "" : s1.FullName.ToString()
                            };

            var auditInfo = new ReceivablesAuditInfoDto
            {
                DateCreated = register.CreationTime,
                LastUpdated = register.LastModificationTime,
                UpdatedBy = register.LastModifierUserId == null ? "" : _lookup_userRepository.FirstOrDefault((long)register.LastModifierUserId)?.FullName,
                Approvals = approvals.ToList(),
                CreatedBy = _lookup_userRepository.FirstOrDefault((long)register.CreatorUserId)?.FullName
            };

            return auditInfo;
        }

        public async Task<GetReceivablesApprovalForViewDto> GetReceivablesApprovalForView(Guid id)
         {
            var receivablesApproval = await _receivablesApprovalRepository.GetAsync(id);

            var output = new GetReceivablesApprovalForViewDto { ReceivablesApproval = ObjectMapper.Map<ReceivablesApprovalDto>(receivablesApproval) };
			
            return output;
         }
		 
		 //[AbpAuthorize(AppPermissions.Pages_ReceivablesApprovals_Edit)]
		 public async Task<GetReceivablesApprovalForEditOutput> GetReceivablesApprovalForEdit(EntityDto<Guid> input)
         {
            var receivablesApproval = await _receivablesApprovalRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetReceivablesApprovalForEditOutput {ReceivablesApproval = ObjectMapper.Map<CreateOrEditReceivablesApprovalDto>(receivablesApproval)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditReceivablesApprovalDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 //[AbpAuthorize(AppPermissions.Pages_ReceivablesApprovals_Create)]
		 protected virtual async Task Create(CreateOrEditReceivablesApprovalDto input)
         {
            var receivablesApproval = ObjectMapper.Map<ReceivablesApproval>(input);

			

            await _receivablesApprovalRepository.InsertAsync(receivablesApproval);
         }

		 //[AbpAuthorize(AppPermissions.Pages_ReceivablesApprovals_Edit)]
		 protected virtual async Task Update(CreateOrEditReceivablesApprovalDto input)
         {
            var receivablesApproval = await _receivablesApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, receivablesApproval);
         }

		 //[AbpAuthorize(AppPermissions.Pages_ReceivablesApprovals_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _receivablesApprovalRepository.DeleteAsync(input.Id);
         } 
    }
}