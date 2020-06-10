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
using TestDemo.Common.Dto;
using TestDemo.EclShared;
using TestDemo.Editions;
using TestDemo.Editions.Dto;

namespace TestDemo.Common
{
    [AbpAuthorize]
    public class CommonLookupAppService : TestDemoAppServiceBase, ICommonLookupAppService
    {
        private readonly EditionManager _editionManager;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<MacroeconomicVariable, int> _macroeconomicVariableRepository;

        public CommonLookupAppService(EditionManager editionManager, 
                                      IRepository<OrganizationUnit, long> organizationUnitRepository,
                                      IRepository<MacroeconomicVariable, int> macroeconomicVariableRepository)
        {
            _editionManager = editionManager;
            _organizationUnitRepository = organizationUnitRepository;
            _macroeconomicVariableRepository = macroeconomicVariableRepository;
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
    }
}
