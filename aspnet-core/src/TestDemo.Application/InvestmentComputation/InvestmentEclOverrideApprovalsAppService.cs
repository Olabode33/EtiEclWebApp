using TestDemo.Authorization.Users;
using TestDemo.InvestmentComputation;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.InvestmentComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.InvestmentComputation
{
	[AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrideApprovals)]
    public class InvestmentEclOverrideApprovalsAppService : TestDemoAppServiceBase, IInvestmentEclOverrideApprovalsAppService
    {
		 private readonly IRepository<InvestmentEclOverrideApproval, Guid> _investmentEclOverrideApprovalRepository;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 private readonly IRepository<InvestmentEclOverride,Guid> _lookup_investmentEclOverrideRepository;
		 

		  public InvestmentEclOverrideApprovalsAppService(IRepository<InvestmentEclOverrideApproval, Guid> investmentEclOverrideApprovalRepository , IRepository<User, long> lookup_userRepository, IRepository<InvestmentEclOverride, Guid> lookup_investmentEclOverrideRepository) 
		  {
			_investmentEclOverrideApprovalRepository = investmentEclOverrideApprovalRepository;
			_lookup_userRepository = lookup_userRepository;
		_lookup_investmentEclOverrideRepository = lookup_investmentEclOverrideRepository;
		
		  }

		 public async Task<PagedResultDto<GetInvestmentEclOverrideApprovalForViewDto>> GetAll(GetAllInvestmentEclOverrideApprovalsInput input)
         {
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredInvestmentEclOverrideApprovals = _investmentEclOverrideApprovalRepository.GetAll()
						.Include( e => e.ReviewedByUserFk)
						.Include( e => e.InvestmentEclOverrideFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ReviewComment.Contains(input.Filter))
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ReviewedByUserFk != null && e.ReviewedByUserFk.Name == input.UserNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.InvestmentEclOverrideOverrideCommentFilter), e => e.InvestmentEclOverrideFk != null && e.InvestmentEclOverrideFk.OverrideComment == input.InvestmentEclOverrideOverrideCommentFilter);

			var pagedAndFilteredInvestmentEclOverrideApprovals = filteredInvestmentEclOverrideApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var investmentEclOverrideApprovals = from o in pagedAndFilteredInvestmentEclOverrideApprovals
                         join o1 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_investmentEclOverrideRepository.GetAll() on o.InvestmentEclOverrideId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetInvestmentEclOverrideApprovalForViewDto() {
							InvestmentEclOverrideApproval = new InvestmentEclOverrideApprovalDto
							{
                                ReviewDate = o.ReviewDate,
                                ReviewComment = o.ReviewComment,
                                Status = o.Status,
                                Id = o.Id
							},
                         	UserName = s1 == null ? "" : s1.Name.ToString(),
                         	InvestmentEclOverrideOverrideComment = s2 == null ? "" : s2.OverrideComment.ToString()
						};

            var totalCount = await filteredInvestmentEclOverrideApprovals.CountAsync();

            return new PagedResultDto<GetInvestmentEclOverrideApprovalForViewDto>(
                totalCount,
                await investmentEclOverrideApprovals.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrideApprovals_Edit)]
		 public async Task<GetInvestmentEclOverrideApprovalForEditOutput> GetInvestmentEclOverrideApprovalForEdit(EntityDto<Guid> input)
         {
            var investmentEclOverrideApproval = await _investmentEclOverrideApprovalRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetInvestmentEclOverrideApprovalForEditOutput {InvestmentEclOverrideApproval = ObjectMapper.Map<CreateOrEditInvestmentEclOverrideApprovalDto>(investmentEclOverrideApproval)};

		    if (output.InvestmentEclOverrideApproval.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.InvestmentEclOverrideApproval.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }

		    if (output.InvestmentEclOverrideApproval.InvestmentEclOverrideId != null)
            {
                var _lookupInvestmentEclOverride = await _lookup_investmentEclOverrideRepository.FirstOrDefaultAsync((Guid)output.InvestmentEclOverrideApproval.InvestmentEclOverrideId);
                output.InvestmentEclOverrideOverrideComment = _lookupInvestmentEclOverride.OverrideComment.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditInvestmentEclOverrideApprovalDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrideApprovals_Create)]
		 protected virtual async Task Create(CreateOrEditInvestmentEclOverrideApprovalDto input)
         {
            var investmentEclOverrideApproval = ObjectMapper.Map<InvestmentEclOverrideApproval>(input);

			

            await _investmentEclOverrideApprovalRepository.InsertAsync(investmentEclOverrideApproval);
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrideApprovals_Edit)]
		 protected virtual async Task Update(CreateOrEditInvestmentEclOverrideApprovalDto input)
         {
            var investmentEclOverrideApproval = await _investmentEclOverrideApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, investmentEclOverrideApproval);
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrideApprovals_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _investmentEclOverrideApprovalRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrideApprovals)]
         public async Task<PagedResultDto<InvestmentEclOverrideApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<InvestmentEclOverrideApprovalUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new InvestmentEclOverrideApprovalUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<InvestmentEclOverrideApprovalUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrideApprovals)]
         public async Task<PagedResultDto<InvestmentEclOverrideApprovalInvestmentEclOverrideLookupTableDto>> GetAllInvestmentEclOverrideForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_investmentEclOverrideRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.OverrideComment.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var investmentEclOverrideList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<InvestmentEclOverrideApprovalInvestmentEclOverrideLookupTableDto>();
			foreach(var investmentEclOverride in investmentEclOverrideList){
				lookupTableDtoList.Add(new InvestmentEclOverrideApprovalInvestmentEclOverrideLookupTableDto
				{
					Id = investmentEclOverride.Id.ToString(),
					DisplayName = investmentEclOverride.OverrideComment?.ToString()
				});
			}

            return new PagedResultDto<InvestmentEclOverrideApprovalInvestmentEclOverrideLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}