
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.LoanImpairmentApprovals.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.Authorization.Users;
using TestDemo.LoanImpairmentsRegisters;

namespace TestDemo.LoanImpairmentApprovals
{
	[AbpAuthorize(AppPermissions.Pages_LoanImpairmentApprovals)]
    public class LoanImpairmentApprovalsAppService : TestDemoAppServiceBase, ILoanImpairmentApprovalsAppService
    {
		 private readonly IRepository<LoanImpairmentApproval, Guid> _loanImpairmentApprovalRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<LoanImpairmentRegister, Guid> _loanImpairmentRegisterRepository;


        public LoanImpairmentApprovalsAppService(IRepository<LoanImpairmentApproval, Guid> loanImpairmentApprovalRepository, IRepository<User, long> lookup_userRepository,
            IRepository<LoanImpairmentRegister, Guid> loanImpairmentRegisterRepository) 
		  {
			_loanImpairmentApprovalRepository = loanImpairmentApprovalRepository;
            _lookup_userRepository = lookup_userRepository;
            _loanImpairmentRegisterRepository = loanImpairmentRegisterRepository;
          }

		 public async Task<PagedResultDto<GetLoanImpairmentApprovalForViewDto>> GetAll(GetAllLoanImpairmentApprovalsInput input)
         {
			
			var filteredLoanImpairmentApprovals = _loanImpairmentApprovalRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ReviewComment.Contains(input.Filter));

			var pagedAndFilteredLoanImpairmentApprovals = filteredLoanImpairmentApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var loanImpairmentApprovals = from o in pagedAndFilteredLoanImpairmentApprovals
                         select new GetLoanImpairmentApprovalForViewDto() {
							LoanImpairmentApproval = new LoanImpairmentApprovalDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredLoanImpairmentApprovals.CountAsync();

            return new PagedResultDto<GetLoanImpairmentApprovalForViewDto>(
                totalCount,
                await loanImpairmentApprovals.ToListAsync()
            );
         }

        public async Task<LoanImpairmentAuditInfoDto> GetLoanImpairmentApprovals(Guid id)
        {
            var holdCoApprovals = await _loanImpairmentApprovalRepository.GetAll().Where(a => a.RegisterId == id).ToListAsync();

            var register = _loanImpairmentRegisterRepository.FirstOrDefault(id);

            var approvals = from o in holdCoApprovals
                            join o1 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o1.Id into j1
                            from s1 in j1.DefaultIfEmpty()

                            select new LoanImpairmentApprovalAuditInfoDto()
                            {
                                RegistrationId = id,
                                ReviewedDate = o.ReviewedDate,
                                Status = o.Status,
                                ReviewComment = o.ReviewComment,
                                ReviewedBy = s1 == null ? "" : s1.FullName.ToString()
                            };

            var auditInfo = new LoanImpairmentAuditInfoDto
            {
                DateCreated = register.CreationTime,
                LastUpdated = register.LastModificationTime,
                UpdatedBy = register.LastModifierUserId == null ? "" : _lookup_userRepository.FirstOrDefault((long)register.LastModifierUserId)?.FullName,
                Approvals = approvals.ToList(),
                CreatedBy = _lookup_userRepository.FirstOrDefault((long)register.CreatorUserId)?.FullName
            };

            return auditInfo;
        }

        public async Task<GetLoanImpairmentApprovalForViewDto> GetLoanImpairmentApprovalForView(Guid id)
         {
            var loanImpairmentApproval = await _loanImpairmentApprovalRepository.GetAsync(id);

            var output = new GetLoanImpairmentApprovalForViewDto { LoanImpairmentApproval = ObjectMapper.Map<LoanImpairmentApprovalDto>(loanImpairmentApproval) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentApprovals_Edit)]
		 public async Task<GetLoanImpairmentApprovalForEditOutput> GetLoanImpairmentApprovalForEdit(EntityDto<Guid> input)
         {
            var loanImpairmentApproval = await _loanImpairmentApprovalRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetLoanImpairmentApprovalForEditOutput {LoanImpairmentApproval = ObjectMapper.Map<CreateOrEditLoanImpairmentApprovalDto>(loanImpairmentApproval)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditLoanImpairmentApprovalDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentApprovals_Create)]
		 protected virtual async Task Create(CreateOrEditLoanImpairmentApprovalDto input)
         {
            var loanImpairmentApproval = ObjectMapper.Map<LoanImpairmentApproval>(input);

			

            await _loanImpairmentApprovalRepository.InsertAsync(loanImpairmentApproval);
         }

		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentApprovals_Edit)]
		 protected virtual async Task Update(CreateOrEditLoanImpairmentApprovalDto input)
         {
            var loanImpairmentApproval = await _loanImpairmentApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, loanImpairmentApproval);
         }

		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentApprovals_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _loanImpairmentApprovalRepository.DeleteAsync(input.Id);
         } 
    }
}