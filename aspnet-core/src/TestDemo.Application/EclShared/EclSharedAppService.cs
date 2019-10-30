﻿using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Text;
using System.Threading.Tasks;
using TestDemo.Authorization.Users;
using TestDemo.EclShared.Dtos;
using TestDemo.OBE;
using TestDemo.Retail;
using TestDemo.Wholesale;
using Microsoft.EntityFrameworkCore;
using Abp.Organizations;

namespace TestDemo.EclShared
{
    public class EclSharedAppService : TestDemoAppServiceBase, IEclSharedAppService
    {
        private readonly IRepository<WholesaleEcl, Guid> _wholesaleEclRepository;
        private readonly IRepository<RetailEcl, Guid> _retailEclRepository;
        private readonly IRepository<ObeEcl, Guid> _obeEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly UserManager _userManager;

        public EclSharedAppService(
            IRepository<WholesaleEcl, Guid> wholesaleEclRepository, 
            IRepository<RetailEcl, Guid> retailEclRepository, 
            IRepository<ObeEcl, Guid> obeEclRepository, 
            IRepository<User, long> lookup_userRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            UserManager userManager)
        {
            _wholesaleEclRepository = wholesaleEclRepository;
            _retailEclRepository = retailEclRepository;
            _obeEclRepository = obeEclRepository;
            _lookup_userRepository = lookup_userRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _userManager = userManager;
        }

        public async Task<PagedResultDto<GetAllEclForWorkspaceDto>> GetAllEclForWorkspace(GetAllForLookupTableInput input)
        {
            var user = await _userManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userOrganizationUnit = await _userManager.GetOrganizationUnitsAsync(user);
            //var userSubsChildren = _organizationUnitRepository.GetAll().Where(ou => userSubsidiaries.Any(uou => ou.Code.StartsWith(uou.Code)));
            var userOrganizationUnitIds = userOrganizationUnit.Select(ou => ou.Id);


            var allEcl = (from w in _wholesaleEclRepository.GetAll().WhereIf(userOrganizationUnitIds.Count() > 0,  x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))

                          join ou in _organizationUnitRepository.GetAll() on w.OrganizationUnitId equals ou.Id
                          
                          join u in _lookup_userRepository.GetAll() on w.CreatorUserId equals u.Id into u1
                          from u2 in u1.DefaultIfEmpty()
                          select new GetAllEclForWorkspaceDto()
                              {
                                  Framework = FrameworkEnum.Wholesale,
                                  CreatedByUserName = u2 == null ? "" : u2.FullName,
                                  DateCreated = w.CreationTime,
                                  ReportingDate = w.ReportingDate,
                                  OrganisationUnitName = ou == null ? "" : ou.DisplayName,
                                  Status = w.Status,
                                  Id = w.Id
                              }
                          );

            var pagedEcls = allEcl
                            .OrderBy(input.Sorting ?? "dateCreated desc")
                            .PageBy(input);

            var totalCount = await allEcl.CountAsync();

            return new PagedResultDto<GetAllEclForWorkspaceDto>(
                totalCount,
                await pagedEcls.ToListAsync()
            );
        }
    }
}
