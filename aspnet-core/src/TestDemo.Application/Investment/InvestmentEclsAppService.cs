using TestDemo.Authorization.Users;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.Investment.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.Investment
{
	[AbpAuthorize(AppPermissions.Pages_InvestmentEcls)]
    public class InvestmentEclsAppService : TestDemoAppServiceBase, IInvestmentEclsAppService
    {
		 private readonly IRepository<InvestmentEcl, Guid> _investmentEclRepository;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 

		  public InvestmentEclsAppService(IRepository<InvestmentEcl, Guid> investmentEclRepository , IRepository<User, long> lookup_userRepository) 
		  {
			_investmentEclRepository = investmentEclRepository;
			_lookup_userRepository = lookup_userRepository;
		
		  }

		 public async Task<PagedResultDto<GetInvestmentEclForViewDto>> GetAll(GetAllInvestmentEclsInput input)
         {
			var statusFilter = (EclStatusEnum) input.StatusFilter;
			
			var filteredInvestmentEcls = _investmentEclRepository.GetAll()
						.Include( e => e.ClosedByUserFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ClosedByUserFk != null && e.ClosedByUserFk.Name == input.UserNameFilter);

			var pagedAndFilteredInvestmentEcls = filteredInvestmentEcls
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var investmentEcls = from o in pagedAndFilteredInvestmentEcls
                         join o1 in _lookup_userRepository.GetAll() on o.ClosedByUserId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetInvestmentEclForViewDto() {
							InvestmentEcl = new InvestmentEclDto
							{
                                ReportingDate = o.ReportingDate,
                                ClosedDate = o.ClosedDate,
                                IsApproved = o.IsApproved,
                                Status = o.Status,
                                Id = o.Id
							},
                         	UserName = s1 == null ? "" : s1.Name.ToString()
						};

            var totalCount = await filteredInvestmentEcls.CountAsync();

            return new PagedResultDto<GetInvestmentEclForViewDto>(
                totalCount,
                await investmentEcls.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_InvestmentEcls_Edit)]
		 public async Task<GetInvestmentEclForEditOutput> GetInvestmentEclForEdit(EntityDto<Guid> input)
         {
            var investmentEcl = await _investmentEclRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetInvestmentEclForEditOutput {InvestmentEcl = ObjectMapper.Map<CreateOrEditInvestmentEclDto>(investmentEcl)};

		    if (output.InvestmentEcl.ClosedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.InvestmentEcl.ClosedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditInvestmentEclDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEcls_Create)]
		 protected virtual async Task Create(CreateOrEditInvestmentEclDto input)
         {
            var investmentEcl = ObjectMapper.Map<InvestmentEcl>(input);

			

            await _investmentEclRepository.InsertAsync(investmentEcl);
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEcls_Edit)]
		 protected virtual async Task Update(CreateOrEditInvestmentEclDto input)
         {
            var investmentEcl = await _investmentEclRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, investmentEcl);
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEcls_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _investmentEclRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_InvestmentEcls)]
         public async Task<PagedResultDto<InvestmentEclUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<InvestmentEclUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new InvestmentEclUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<InvestmentEclUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}