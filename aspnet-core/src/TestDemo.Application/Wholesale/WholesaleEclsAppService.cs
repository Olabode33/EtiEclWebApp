using TestDemo.Authorization.Users;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.Wholesale.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.WholesaleInputs;
using System.Data;
using TestDemo.Utils;

namespace TestDemo.Wholesale
{
    [AbpAuthorize(AppPermissions.Pages_WholesaleEcls)]
    public class WholesaleEclsAppService : TestDemoAppServiceBase, IWholesaleEclsAppService
    {
        private readonly IRepository<WholesaleEcl, Guid> _wholesaleEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly UserManager _userManager;
        private readonly IRepository<WholesaleEclUpload, Guid> _wholesaleEclUploadRepository;
        private readonly IRepository<WholesaleEclDataLoanBook, Guid> _wholesaleEclLoanBookRepository;


        public WholesaleEclsAppService(
            IRepository<WholesaleEcl, Guid> wholesaleEclRepository, 
            IRepository<User, long> lookup_userRepository,
            UserManager userManager,
            IRepository<WholesaleEclUpload, Guid> wholesaleEclUploadRepository,
            IRepository<WholesaleEclDataLoanBook, Guid> wholesaleEclLoanBookRepository)
        {
            _wholesaleEclRepository = wholesaleEclRepository;
            _lookup_userRepository = lookup_userRepository;
            _userManager = userManager;
            _wholesaleEclUploadRepository = wholesaleEclUploadRepository;
            _wholesaleEclLoanBookRepository = wholesaleEclLoanBookRepository;
        }

        public async Task<PagedResultDto<GetWholesaleEclForViewDto>> GetAll(GetAllWholesaleEclsInput input)
        {
            var statusFilter = (EclStatusEnum)input.StatusFilter;

            var filteredWholesaleEcls = _wholesaleEclRepository.GetAll()
                        .Include(e => e.ClosedByUserFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinReportingDateFilter != null, e => e.ReportingDate >= input.MinReportingDateFilter)
                        .WhereIf(input.MaxReportingDateFilter != null, e => e.ReportingDate <= input.MaxReportingDateFilter)
                        .WhereIf(input.MinClosedDateFilter != null, e => e.ClosedDate >= input.MinClosedDateFilter)
                        .WhereIf(input.MaxClosedDateFilter != null, e => e.ClosedDate <= input.MaxClosedDateFilter)
                        .WhereIf(input.IsApprovedFilter > -1, e => Convert.ToInt32(e.IsApproved) == input.IsApprovedFilter)
                        .WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ClosedByUserFk != null && e.ClosedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

            var pagedAndFilteredWholesaleEcls = filteredWholesaleEcls
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var wholesaleEcls = from o in pagedAndFilteredWholesaleEcls
                                join o1 in _lookup_userRepository.GetAll() on o.ClosedByUserId equals o1.Id into j1
                                from s1 in j1.DefaultIfEmpty()

                                select new GetWholesaleEclForViewDto()
                                {
                                    WholesaleEcl = new WholesaleEclDto
                                    {
                                        ReportingDate = o.ReportingDate,
                                        ClosedDate = o.ClosedDate,
                                        IsApproved = o.IsApproved,
                                        Status = o.Status,
                                        Id = o.Id
                                    },
                                    UserName = s1 == null ? "" : s1.Name.ToString()
                                };

            var totalCount = await filteredWholesaleEcls.CountAsync();

            return new PagedResultDto<GetWholesaleEclForViewDto>(
                totalCount,
                await wholesaleEcls.ToListAsync()
            );
        }

        [AbpAuthorize(AppPermissions.Pages_WholesaleEcls_Edit)]
        public async Task<GetWholesaleEclForEditOutput> GetWholesaleEclForEdit(EntityDto<Guid> input)
        {
            var wholesaleEcl = await _wholesaleEclRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetWholesaleEclForEditOutput { WholesaleEcl = ObjectMapper.Map<CreateOrEditWholesaleEclDto>(wholesaleEcl) };

            if (output.WholesaleEcl.ClosedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.WholesaleEcl.ClosedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditWholesaleEclDto input)
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

        [AbpAuthorize(AppPermissions.Pages_WholesaleEcls_Create)]
        protected virtual async Task Create(CreateOrEditWholesaleEclDto input)
        {
            var wholesaleEcl = ObjectMapper.Map<WholesaleEcl>(input);

            var user = await _userManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userSubsidiaries = await _userManager.GetOrganizationUnitsAsync(user);

            if (userSubsidiaries.Count > 0)
            {
                wholesaleEcl.OrganizationUnitId = userSubsidiaries[0].Id;
            }

            if (AbpSession.TenantId != null)
            {
                wholesaleEcl.TenantId = (int?)AbpSession.TenantId;
            }

            await _wholesaleEclRepository.InsertAsync(wholesaleEcl);
        }

        [AbpAuthorize(AppPermissions.Pages_WholesaleEcls_Edit)]
        protected virtual async Task Update(CreateOrEditWholesaleEclDto input)
        {
            var wholesaleEcl = await _wholesaleEclRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, wholesaleEcl);
        }

        [AbpAuthorize(AppPermissions.Pages_WholesaleEcls_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _wholesaleEclRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_WholesaleEcls)]
        public async Task<PagedResultDto<WholesaleEclUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_userRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Name.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<WholesaleEclUserLookupTableDto>();
            foreach (var user in userList)
            {
                lookupTableDtoList.Add(new WholesaleEclUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user.Name?.ToString()
                });
            }

            return new PagedResultDto<WholesaleEclUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        public async Task<string> RunEcl(EntityDto<Guid> input)
        {
            var uploadedData = await _wholesaleEclUploadRepository.FirstOrDefaultAsync(x => x.WholesaleEclId == input.Id);
            if (uploadedData == null)
            {
                return "No upload";
            }

            var loanbookData = await _wholesaleEclLoanBookRepository.GetAll().Where(x => x.WholesaleEclUploadId == uploadedData.Id).ToListAsync();
            if (loanbookData.Count == 0)
            {
                return "No Data";
            }

            DataTable loanbookDatatable = DataTableUtil.ToDataTable(loanbookData);

            return "Sample";
            //PdInputEngine PdInputEngine = new PdInputEngine();
            //return PdInputEngine.RunPdEngine(loanbookDatatable);
        }
    }
}