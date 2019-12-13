using TestDemo.Authorization.Users;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.Retail.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TestDemo.RetailAssumption;
using TestDemo.EclShared.Dtos;
using TestDemo.RetailAssumption.Dtos;
using Abp.Organizations;
using Abp.BackgroundJobs;
using TestDemo.EclLibrary.BaseEngine.PDInput;

namespace TestDemo.Retail
{
    [AbpAuthorize(AppPermissions.Pages_RetailEcls)]
    public class RetailEclsAppService : TestDemoAppServiceBase, IRetailEclsAppService
    {
        private readonly IRepository<RetailEcl, Guid> _retailEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRetailEclAssumptionsAppService _retailEclAssumptionAppService;
        private readonly IRetailEclEadInputAssumptionsAppService _retailEclEadInputAssumptionsAppService;
        private readonly IRetailEclLgdAssumptionsAppService _retailEclLgdAssumptionsAppService;
        private readonly IRetailEclApprovalsAppService _retailEclApprovalsAppService;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public RetailEclsAppService(IRepository<RetailEcl, Guid> retailEclRepository,
                                    IRepository<User, long> lookup_userRepository,
                                    IRepository<OrganizationUnit, long> organizationUnitRepository,
                                    IRetailEclAssumptionsAppService retailEclAssumptionAppService,
                                    IRetailEclEadInputAssumptionsAppService retailEclEadInputAssumptionsAppService,
                                    IRetailEclLgdAssumptionsAppService retailEclLgdAssumptionsAppService,
                                    IRetailEclApprovalsAppService retailEclApprovalsAppService,
                                    IBackgroundJobManager backgroundJobManager
                                    )
        {
            _retailEclRepository = retailEclRepository;
            _lookup_userRepository = lookup_userRepository;
            _retailEclAssumptionAppService = retailEclAssumptionAppService;
            _retailEclEadInputAssumptionsAppService = retailEclEadInputAssumptionsAppService;
            _retailEclLgdAssumptionsAppService = retailEclLgdAssumptionsAppService;
            _retailEclApprovalsAppService = retailEclApprovalsAppService;
            _organizationUnitRepository = organizationUnitRepository;
            _backgroundJobManager = backgroundJobManager;
        }

        public async Task<PagedResultDto<GetRetailEclForViewDto>> GetAll(GetAllRetailEclsInput input)
        {
            var statusFilter = (EclStatusEnum)input.StatusFilter;

            var filteredRetailEcls = _retailEclRepository.GetAll()
                        .Include(e => e.ClosedByUserFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinReportingDateFilter != null, e => e.ReportingDate >= input.MinReportingDateFilter)
                        .WhereIf(input.MaxReportingDateFilter != null, e => e.ReportingDate <= input.MaxReportingDateFilter)
                        .WhereIf(input.MinClosedDateFilter != null, e => e.ClosedDate >= input.MinClosedDateFilter)
                        .WhereIf(input.MaxClosedDateFilter != null, e => e.ClosedDate <= input.MaxClosedDateFilter)
                        .WhereIf(input.IsApprovedFilter > -1, e => Convert.ToInt32(e.IsApproved) == input.IsApprovedFilter)
                        .WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ClosedByUserFk != null && e.ClosedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

            var pagedAndFilteredRetailEcls = filteredRetailEcls
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var retailEcls = from o in pagedAndFilteredRetailEcls
                             join o1 in _lookup_userRepository.GetAll() on o.ClosedByUserId equals o1.Id into j1
                             from s1 in j1.DefaultIfEmpty()

                             select new GetRetailEclForViewDto()
                             {
                                 RetailEcl = new RetailEclDto
                                 {
                                     ReportingDate = o.ReportingDate,
                                     ClosedDate = o.ClosedDate,
                                     IsApproved = o.IsApproved,
                                     Status = o.Status,
                                     Id = o.Id
                                 },
                                 UserName = s1 == null ? "" : s1.Name.ToString()
                             };

            var totalCount = await filteredRetailEcls.CountAsync();

            return new PagedResultDto<GetRetailEclForViewDto>(
                totalCount,
                await retailEcls.ToListAsync()
            );
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEcls_Edit)]
        public async Task<GetRetailEclForEditOutput> GetRetailEclForEdit(EntityDto<Guid> input)
        {
            var retailEcl = await _retailEclRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRetailEclForEditOutput { RetailEcl = ObjectMapper.Map<CreateOrEditRetailEclDto>(retailEcl) };

            if (output.RetailEcl.ClosedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.RetailEcl.ClosedByUserId);
                output.ClosedByUserName = _lookupUser.Name.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEcls_Edit)]
        public async Task<GetRetailEclForEditOutput> GetRetailEclDetailsForEdit(EntityDto<Guid> input)
        {
            var retailEcl = await _retailEclRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRetailEclForEditOutput { RetailEcl = ObjectMapper.Map<CreateOrEditRetailEclDto>(retailEcl) };
            if (retailEcl.CreatorUserId != null)
            {
                var _creatorUser = await _lookup_userRepository.FirstOrDefaultAsync((long)retailEcl.CreatorUserId);
                output.CreatedByUserName = _creatorUser.FullName.ToString();
            }

            if (retailEcl.OrganizationUnitId != null)
            {
                var ou = await _organizationUnitRepository.FirstOrDefaultAsync((long)retailEcl.OrganizationUnitId);
                output.Country = ou.DisplayName;
            }

            if (output.RetailEcl.ClosedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.RetailEcl.ClosedByUserId);
                output.ClosedByUserName = _lookupUser.FullName.ToString();
            }

            output.FrameworkAssumption = await _retailEclAssumptionAppService.GetRetailEclAssumptionsList(input);
            output.EadInputAssumptions = await _retailEclEadInputAssumptionsAppService.GetRetailEclEadInputAssumptionsList(input);
            output.LgdInputAssumptions = await _retailEclLgdAssumptionsAppService.GetRetailEclLgdAssumptionsList(input);

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditRetailEclDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        public async Task CreateEclAndAssumption(CreateRetailEclAndAssumptions input)
        {
            Guid eclId = await CreateAndGetId(input.RetailEcl);
            await SaveFrameworkAssumption(input.FrameworkAssumptions, eclId);
            await SaveEadInputAssumption(input.EadInputAssumptionDtos, eclId);
            await SaveLgdInputAssumption(input.LgdInputAssumptionDtos, eclId);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEcls_Create)]
        protected virtual async Task Create(CreateOrEditRetailEclDto input)
        {
            var retailEcl = ObjectMapper.Map<RetailEcl>(input);

            var user = await UserManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userSubsidiaries = await UserManager.GetOrganizationUnitsAsync(user);

            if (userSubsidiaries.Count > 0)
            {
                retailEcl.OrganizationUnitId = userSubsidiaries[0].Id;
            }

            if (AbpSession.TenantId != null)
            {
                retailEcl.TenantId = (int?)AbpSession.TenantId;
            }


            await _retailEclRepository.InsertAsync(retailEcl);
        }

        protected virtual async Task<Guid> CreateAndGetId(CreateOrEditRetailEclDto input)
        {
            var retailEcl = ObjectMapper.Map<RetailEcl>(input);

            var user = await UserManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userSubsidiaries = await UserManager.GetOrganizationUnitsAsync(user);

            if (userSubsidiaries.Count > 0)
            {
                retailEcl.OrganizationUnitId = userSubsidiaries[0].Id;
            }

            if (AbpSession.TenantId != null)
            {
                retailEcl.TenantId = (int?)AbpSession.TenantId;
            }


            Guid id = await _retailEclRepository.InsertAndGetIdAsync(retailEcl);
            return id;
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEcls_Edit)]
        protected virtual async Task Update(CreateOrEditRetailEclDto input)
        {
            var retailEcl = await _retailEclRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, retailEcl);
        }

        protected virtual async Task SaveFrameworkAssumption(List<AssumptionDto> assumptions, Guid eclId)
        {
            foreach (var assumption in assumptions)
            {
                await _retailEclAssumptionAppService.CreateOrEdit(new CreateOrEditRetailEclAssumptionDto()
                                                                        {
                                                                            RetailEclId = eclId,
                                                                            AssumptionGroup = assumption.AssumptionGroup,
                                                                            Key = assumption.Key,
                                                                            InputName = assumption.InputName,
                                                                            Value = assumption.Value,
                                                                            Datatype = assumption.DataType,
                                                                            IsComputed = assumption.IsComputed,
                                                                            RequiresGroupApproval = assumption.RequiresGroupApproval
                                                                        });
            }
        }

        protected virtual async Task SaveEadInputAssumption(List<EadInputAssumptionDto> assumptions, Guid eclId)
        {
            foreach (var assumption in assumptions)
            {
                await _retailEclEadInputAssumptionsAppService.CreateOrEdit(new CreateOrEditRetailEclEadInputAssumptionDto()
                {
                    RetailEclId = eclId,
                    EadGroup = assumption.AssumptionGroup,
                    Key = assumption.Key,
                    InputName = assumption.InputName,
                    Value = assumption.Value,
                    Datatype = assumption.DataType,
                    IsComputed = assumption.IsComputed,
                    RequiresGroupApproval = assumption.RequiresGroupApproval
                });
            }
        }

        protected virtual async Task SaveLgdInputAssumption(List<LgdAssumptionDto> assumptions, Guid eclId)
        {
            foreach (var assumption in assumptions)
            {
                await _retailEclLgdAssumptionsAppService.CreateOrEdit(new CreateOrEditRetailEclLgdAssumptionDto()
                {
                    RetailEclId = eclId,
                    LgdGroup = assumption.AssumptionGroup,
                    Key = assumption.Key,
                    InputName = assumption.InputName,
                    Value = assumption.Value,
                    DataType = assumption.DataType,
                    IsComputed = assumption.IsComputed,
                    RequiresGroupApproval = assumption.RequiresGroupApproval
                });
            }
        }


        public virtual async Task ApproveRejectEcl(CreateOrEditRetailEclApprovalDto input)
        {
            var ecl = await _retailEclRepository.FirstOrDefaultAsync((Guid)input.RetailEclId);
            ecl.Status = input.Status == GeneralStatusEnum.Approved ? EclStatusEnum.Approved : EclStatusEnum.Draft;
            ObjectMapper.Map(ecl, ecl);
            await _retailEclApprovalsAppService.CreateOrEdit(input);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEcls_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _retailEclRepository.DeleteAsync(input.Id);
        }

        public async Task RunEcl(EntityDto<Guid> input)
        {
            await _backgroundJobManager.EnqueueAsync<RunRetailPdJob, RetailPdJobArgs>(new RetailPdJobArgs { RetailEclId = input.Id });
        }

        //[AbpAuthorize(AppPermissions.Pages_RetailEcls)]
        //public async Task<PagedResultDto<RetailEclUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
        //{
        //    var query = _lookup_userRepository.GetAll().WhereIf(
        //           !string.IsNullOrWhiteSpace(input.Filter),
        //          e => e.Name.ToString().Contains(input.Filter)
        //       );

        //    var totalCount = await query.CountAsync();

        //    var userList = await query
        //        .PageBy(input)
        //        .ToListAsync();

        //    var lookupTableDtoList = new List<RetailEclUserLookupTableDto>();
        //    foreach (var user in userList)
        //    {
        //        lookupTableDtoList.Add(new RetailEclUserLookupTableDto
        //        {
        //            Id = user.Id,
        //            DisplayName = user.Name?.ToString()
        //        });
        //    }

        //    return new PagedResultDto<RetailEclUserLookupTableDto>(
        //        totalCount,
        //        lookupTableDtoList
        //    );
        //}
    }
}