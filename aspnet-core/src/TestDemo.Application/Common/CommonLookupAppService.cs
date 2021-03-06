﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Organizations;
using Microsoft.EntityFrameworkCore;
using TestDemo.Authorization.Users;
using TestDemo.Common.Dto;
using TestDemo.Dto.Inputs;
using TestDemo.EclConfig;
using TestDemo.EclLibrary.Workers.Trackers;
using TestDemo.EclShared;
using TestDemo.Editions;
using TestDemo.Editions.Dto;
using TestDemo.Investment;
using TestDemo.OBE;
using TestDemo.Retail;
using TestDemo.Wholesale;

namespace TestDemo.Common
{
    [AbpAuthorize]
    public class CommonLookupAppService : TestDemoAppServiceBase, ICommonLookupAppService
    {
        private readonly EditionManager _editionManager;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<MacroeconomicVariable, int> _macroeconomicVariableRepository;
        private readonly IRepository<TrackFacilityStage> _facilityStageTrackerRepository;
        private readonly IRepository<RetailEcl, Guid> _retailEclRepository;
        private readonly IRepository<ObeEcl, Guid> _obeEclRepository;
        private readonly IRepository<InvestmentEcl, Guid> _investmentEclRepository;
        private readonly IRepository<WholesaleEcl, Guid> _wholesaleEclRepository;
        private readonly IRepository<OverrideType> _overrideTypeRepository;
        private readonly IRepository<TrackRunningUploadJobs> _uploadJobsTrackerRepository;
        private readonly IRepository<TrackCalibrationUploadSummary> _calibrationUploadSummaryRepository;
        private readonly IRepository<TrackMacroUploadSummary> _macroUploadSummaryRepository;
        private readonly IRepository<User, long> _userRepository;

        public CommonLookupAppService(EditionManager editionManager,
                                      IRepository<OrganizationUnit, long> organizationUnitRepository,
                                      IRepository<MacroeconomicVariable, int> macroeconomicVariableRepository,
                                      IRepository<ObeEcl, Guid> obeEclRepository,
                                      IRepository<RetailEcl, Guid> retailEclRepository,
                                      IRepository<WholesaleEcl, Guid> wholesaleRepository,
                                      IRepository<InvestmentEcl, Guid> investmentEclRepository, 
                                      IRepository<OverrideType> overrideTypeRepository,
                                      IRepository<TrackRunningUploadJobs> uploadJobsTrackerRepository,
                                      IRepository<TrackMacroUploadSummary> macroUploadSummaryRepository,
                                      IRepository<TrackCalibrationUploadSummary> calibrationUploadSummaryRepository,
                                      IRepository<User, long> userRepository,
                                      IRepository<TrackFacilityStage> facilityStageTrackerRepository)
        {
            _editionManager = editionManager;
            _organizationUnitRepository = organizationUnitRepository;
            _macroeconomicVariableRepository = macroeconomicVariableRepository;
            _facilityStageTrackerRepository = facilityStageTrackerRepository;
            _wholesaleEclRepository = wholesaleRepository;
            _retailEclRepository = retailEclRepository;
            _obeEclRepository = obeEclRepository;
            _investmentEclRepository = investmentEclRepository;
            _overrideTypeRepository = overrideTypeRepository;
            _uploadJobsTrackerRepository = uploadJobsTrackerRepository;
            _calibrationUploadSummaryRepository = calibrationUploadSummaryRepository;
            _macroUploadSummaryRepository = macroUploadSummaryRepository;
            _userRepository = userRepository;
        }

        public async Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false)
        {
            var subscribableEditions = (await _editionManager.Editions.Cast<SubscribableEdition>().ToListAsync())
                .WhereIf(onlyFreeItems, e => e.IsFree)
                .OrderBy(e => e.MonthlyPrice);

            return new ListResultDto<SubscribableEditionComboboxItemDto>(
                subscribableEditions.Select(e => new SubscribableEditionComboboxItemDto(e.Id.ToString(), e.DisplayName, e.IsFree)).ToList()
            );
        }

        public async Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input)
        {
            if (AbpSession.TenantId != null)
            {
                //Prevent tenants to get other tenant's users.
                input.TenantId = AbpSession.TenantId;
            }

            using (CurrentUnitOfWork.SetTenantId(input.TenantId))
            {
                var query = UserManager.Users
                    .WhereIf(
                        !input.Filter.IsNullOrWhiteSpace(),
                        u =>
                            u.Name.Contains(input.Filter) ||
                            u.Surname.Contains(input.Filter) ||
                            u.UserName.Contains(input.Filter) ||
                            u.EmailAddress.Contains(input.Filter)
                    );

                var userCount = await query.CountAsync();
                var users = await query
                    .OrderBy(u => u.Name)
                    .ThenBy(u => u.Surname)
                    .PageBy(input)
                    .ToListAsync();

                return new PagedResultDto<NameValueDto>(
                    userCount,
                    users.Select(u =>
                        new NameValueDto(
                            u.FullName + " (" + u.EmailAddress + ")",
                            u.Id.ToString()
                            )
                        ).ToList()
                    );
            }
        }

        public GetDefaultEditionNameOutput GetDefaultEditionName()
        {
            return new GetDefaultEditionNameOutput
            {
                Name = EditionManager.DefaultEditionName
            };
        }

        public async Task<string> GetAffiliateNameFromId(long ouId)
        {
            var ou = await _organizationUnitRepository.FirstOrDefaultAsync(ouId);
            return ou.DisplayName;
        }

        public async Task<List<NameValueDto<long>>> GetUserAffiliates()
        {
            var user = await UserManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userSubsidiaries = await UserManager.GetOrganizationUnitsAsync(user);

            List<NameValueDto<long>> userOus = new List<NameValueDto<long>>();

            if (userSubsidiaries.Count > 0)
            {
                foreach (var item in userSubsidiaries)
                {
                    userOus.Add(new NameValueDto<long>
                    {
                        Name = item.DisplayName,
                        Value = item.Id
                    });
                }
            }
            else
            {
                userOus.Add(new NameValueDto<long>
                {
                    Name = "Group",
                    Value = -1
                });
            }

            return userOus;
        }

        public async Task<PagedResultDto<NameValueDto<long>>> GetAllOrganizationUnitForLookupTable(GetForLookupTableInput input)
        {
            var query = _organizationUnitRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => (e.DisplayName != null ? e.DisplayName.ToLower().ToString() : "").Contains(input.Filter.ToLower())
               );

            var totalCount = await query.CountAsync();

            var organizationUnitList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<NameValueDto<long>>();
            foreach (var organizationUnit in organizationUnitList)
            {
                lookupTableDtoList.Add(new NameValueDto<long>
                {
                    Value = organizationUnit.Id,
                    Name = organizationUnit.DisplayName?.ToString()
                });
            }

            return new PagedResultDto<NameValueDto<long>>(
                totalCount,
                lookupTableDtoList
            );
        }

        public async Task<List<NameValueDto>> GetMacroeconomicVariableList()
        {
            return await _macroeconomicVariableRepository.GetAll()
                                                         .Select(x => new NameValueDto()
                                                         {
                                                             Name = x.Name,
                                                             Value = x.Id.ToString()
                                                         }).ToListAsync();
        }

        public async Task<FacilityStageTrackerOutputDto> GetTrackedFacilityInfo(GetFacilityStageTrackerInputDto input)
        {
            long ouId = -1;

            switch (input.Framework)
            {
                case FrameworkEnum.Wholesale:
                    var w_ecl = await _wholesaleEclRepository.FirstOrDefaultAsync(input.EclId);
                    ouId = w_ecl.OrganizationUnitId;
                    break;
                case FrameworkEnum.Retail:
                    var r_ecl = await _retailEclRepository.FirstOrDefaultAsync(input.EclId);
                    ouId = r_ecl.OrganizationUnitId;
                    break;
                case FrameworkEnum.OBE:
                    var o_ecl = await _obeEclRepository.FirstOrDefaultAsync(input.EclId);
                    ouId = o_ecl.OrganizationUnitId;
                    break;
                case FrameworkEnum.Investments:
                    var i_ecl = await _investmentEclRepository.FirstOrDefaultAsync(input.EclId);
                    ouId = i_ecl.OrganizationUnitId;
                    break;
                default:
                    break;
            }


            var trackedFacility = await _facilityStageTrackerRepository.FirstOrDefaultAsync(e => e.Facility == input.Facility && e.Framework == input.Framework && e.OrganizationUnitId == ouId);

            if (trackedFacility != null)
            {
                return new FacilityStageTrackerOutputDto
                {
                    Facility = trackedFacility.Facility,
                    Stage = trackedFacility.Stage,
                    Framework = trackedFacility.Framework,
                    LastReportingDate = trackedFacility.LastReportingDate,
                    OrganizationUnitId = trackedFacility.OrganizationUnitId
                };
            }
            else
            {
                return new FacilityStageTrackerOutputDto
                {
                    Facility = null,
                    Stage = -1
                };
            }
        }

        public async Task<List<NameValueDto<int>>> GetOverrideTypesForDropdown()
        {
            var overrideTypes = _overrideTypeRepository.GetAll().Select(e => new NameValueDto<int>
            {
                Name = e.Name,
                Value = e.Id
            });

            return await overrideTypes.ToListAsync();
        }

        public async Task<int> GetCompletedUploadJobs(EntityDto<Guid> input)
        {
            var completedJobs = await _uploadJobsTrackerRepository.CountAsync(e => e.RegisterId == input.Id);
            return completedJobs;
        }

        public async Task<GetCalibrationUploadSummaryDto> GetCalibrationUploadSummary(EntityDto<Guid> input)
        {
            var summary = await _calibrationUploadSummaryRepository.FirstOrDefaultAsync(e => e.RegisterId == input.Id);
            if (summary == null)
            {
                return null;
            } 
            else
            {
                return new GetCalibrationUploadSummaryDto
                {
                    RegisterId = summary.RegisterId,
                    AllJobs = summary.AllJobs,
                    Comment = summary.Comment,
                    CompletedJobs = summary.CompletedJobs,
                    Status = summary.Status
                };
            }
        }

        public async Task<GetMacroUploadSummaryDto> GetMacroUploadSummary(EntityDto input)
        {
            var summary = await _macroUploadSummaryRepository.FirstOrDefaultAsync(e => e.RegisterId == input.Id);
            if (summary == null)
            {
                return null;
            }
            else
            {
                return new GetMacroUploadSummaryDto
                {
                    RegisterId = summary.RegisterId,
                    AllJobs = summary.AllJobs,
                    Comment = summary.Comment,
                    CompletedJobs = summary.CompletedJobs,
                    Status = summary.Status
                };
            }
        }

        [AbpAllowAnonymous]
        public async Task<bool> CheckUserExist(string userNameOrEmail)
        {
            var user = await _userRepository.GetAll().AnyAsync(e => (e.UserName.ToLower() == userNameOrEmail.ToLower() || e.EmailAddress == userNameOrEmail.ToLower()));// && e.IsApproved == true);
            return user;
        }
    }
}
