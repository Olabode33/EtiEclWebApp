using TestDemo.Retail;

using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailInputs.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.RetailInputs
{
    [AbpAuthorize(AppPermissions.Pages_RetailEclUploads)]
    public class RetailEclUploadsAppService : TestDemoAppServiceBase, IRetailEclUploadsAppService
    {
        private readonly IRepository<RetailEclUpload, Guid> _retailEclUploadRepository;
        private readonly IRepository<RetailEcl, Guid> _lookup_retailEclRepository;


        public RetailEclUploadsAppService(IRepository<RetailEclUpload, Guid> retailEclUploadRepository, IRepository<RetailEcl, Guid> lookup_retailEclRepository)
        {
            _retailEclUploadRepository = retailEclUploadRepository;
            _lookup_retailEclRepository = lookup_retailEclRepository;

        }

        public async Task<PagedResultDto<GetRetailEclUploadForViewDto>> GetAll(GetAllRetailEclUploadsInput input)
        {
            var docTypeFilter = (UploadDocTypeEnum)input.DocTypeFilter;
            var statusFilter = (GeneralStatusEnum)input.StatusFilter;

            var filteredRetailEclUploads = _retailEclUploadRepository.GetAll()
                        .Include(e => e.RetailEclFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.UploadComment.Contains(input.Filter))
                        .WhereIf(input.DocTypeFilter > -1, e => e.DocType == docTypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UploadCommentFilter), e => e.UploadComment.ToLower() == input.UploadCommentFilter.ToLower().Trim())
                        .WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter);

            var pagedAndFilteredRetailEclUploads = filteredRetailEclUploads
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var retailEclUploads = from o in pagedAndFilteredRetailEclUploads
                                   join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                                   from s1 in j1.DefaultIfEmpty()

                                   select new GetRetailEclUploadForViewDto()
                                   {
                                       RetailEclUpload = new RetailEclUploadDto
                                       {
                                           DocType = o.DocType,
                                           UploadComment = o.UploadComment,
                                           Status = o.Status,
                                           Id = o.Id
                                       },
                                       RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
                                   };

            var totalCount = await filteredRetailEclUploads.CountAsync();

            return new PagedResultDto<GetRetailEclUploadForViewDto>(
                totalCount,
                await retailEclUploads.ToListAsync()
            );
        }



        [AbpAuthorize(AppPermissions.Pages_RetailEclUploads_Edit)]
        public async Task<GetRetailEclUploadForEditOutput> GetRetailEclUploadForEdit(EntityDto<Guid> input)
        {
            var retailEclUpload = await _retailEclUploadRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRetailEclUploadForEditOutput { RetailEclUpload = ObjectMapper.Map<CreateOrEditRetailEclUploadDto>(retailEclUpload) };

            if (output.RetailEclUpload.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclUpload.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditRetailEclUploadDto input)
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

        [AbpAuthorize(AppPermissions.Pages_RetailEclUploads_Create)]
        protected virtual async Task Create(CreateOrEditRetailEclUploadDto input)
        {
            var retailEclUpload = ObjectMapper.Map<RetailEclUpload>(input);


            if (AbpSession.TenantId != null)
            {
                retailEclUpload.TenantId = (int?)AbpSession.TenantId;
            }


            await _retailEclUploadRepository.InsertAsync(retailEclUpload);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEclUploads_Edit)]
        protected virtual async Task Update(CreateOrEditRetailEclUploadDto input)
        {
            var retailEclUpload = await _retailEclUploadRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, retailEclUpload);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEclUploads_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _retailEclUploadRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEclUploads)]
        public async Task<PagedResultDto<RetailEclUploadRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_retailEclRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.TenantId.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<RetailEclUploadRetailEclLookupTableDto>();
            foreach (var retailEcl in retailEclList)
            {
                lookupTableDtoList.Add(new RetailEclUploadRetailEclLookupTableDto
                {
                    Id = retailEcl.Id.ToString(),
                    DisplayName = retailEcl.TenantId?.ToString()
                });
            }

            return new PagedResultDto<RetailEclUploadRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}