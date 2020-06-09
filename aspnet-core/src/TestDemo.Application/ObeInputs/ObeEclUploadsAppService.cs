using TestDemo.OBE;

using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ObeInputs.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.Authorization.Users;
using TestDemo.Dto.Inputs;
using Abp.UI;

namespace TestDemo.ObeInputs
{
    [AbpAuthorize(AppPermissions.Pages_ObeEclUploads)]
    public class ObeEclUploadsAppService : TestDemoAppServiceBase, IObeEclUploadsAppService
    {
        private readonly IRepository<ObeEclUpload, Guid> _obeEclUploadRepository;
        private readonly IRepository<ObeEcl, Guid> _lookup_obeEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;


        public ObeEclUploadsAppService(
            IRepository<ObeEclUpload, Guid> obeEclUploadRepository,
            IRepository<ObeEcl, Guid> lookup_obeEclRepository,
            IRepository<User, long> lookup_userRepository)
        {
            _obeEclUploadRepository = obeEclUploadRepository;
            _lookup_obeEclRepository = lookup_obeEclRepository;
            _lookup_userRepository = lookup_userRepository;
        }

        public async Task<PagedResultDto<GetObeEclUploadForViewDto>> GetAll(GetAllObeEclUploadsInput input)
        {
            var docTypeFilter = (UploadDocTypeEnum)input.DocTypeFilter;
            var statusFilter = (GeneralStatusEnum)input.StatusFilter;

            var filteredObeEclUploads = _obeEclUploadRepository.GetAll()
                        .Include(e => e.ObeEclFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.UploadComment.Contains(input.Filter))
                        .WhereIf(input.DocTypeFilter > -1, e => e.DocType == docTypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UploadCommentFilter), e => e.UploadComment.ToLower() == input.UploadCommentFilter.ToLower().Trim())
                        .WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter);

            var pagedAndFilteredObeEclUploads = filteredObeEclUploads
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var obeEclUploads = from o in pagedAndFilteredObeEclUploads
                                join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                                from s1 in j1.DefaultIfEmpty()

                                select new GetObeEclUploadForViewDto()
                                {
                                    ObeEclUpload = new ObeEclUploadDto
                                    {
                                        DocType = o.DocType,
                                        UploadComment = o.UploadComment,
                                        Status = o.Status,
                                        Id = o.Id
                                    },
                                    ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
                                };

            var totalCount = await filteredObeEclUploads.CountAsync();

            return new PagedResultDto<GetObeEclUploadForViewDto>(
                totalCount,
                await obeEclUploads.ToListAsync()
            );
        }

        public async Task<List<GetEclUploadForViewDto>> GetEclUploads(EntityDto<Guid> input)
        {
            var eclUploads = from o in _obeEclUploadRepository.GetAll().Where(x => x.ObeEclId == input.Id)
                             join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                             from s1 in j1.DefaultIfEmpty()

                             join u in _lookup_userRepository.GetAll() on o.CreatorUserId equals u.Id into u1
                             from u2 in u1.DefaultIfEmpty()

                             select new GetEclUploadForViewDto()
                             {
                                 EclUpload = new EclUploadDto
                                 {
                                     DocType = o.DocType,
                                     UploadComment = o.UploadComment,
                                     Status = o.Status,
                                     EclId = (Guid)o.ObeEclId,
                                     Id = o.Id
                                 },
                                 DateUploaded = o.CreationTime,
                                 UploadedBy = u2 == null ? "" : u2.FullName
                             };

            return await eclUploads.ToListAsync();
        }


        [AbpAuthorize(AppPermissions.Pages_ObeEclUploads_Edit)]
        public async Task<GetObeEclUploadForEditOutput> GetObeEclUploadForEdit(EntityDto<Guid> input)
        {
            var obeEclUpload = await _obeEclUploadRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetObeEclUploadForEditOutput { ObeEclUpload = ObjectMapper.Map<CreateOrEditObeEclUploadDto>(obeEclUpload) };

            if (output.ObeEclUpload.EclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclUpload.EclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }

            return output;
        }

        public async Task<Guid> CreateOrEdit(CreateOrEditObeEclUploadDto input)
        {
            if (input.Id == null)
            {
                var id = await Create(input);
                return id;
            }
            else
            {
                await Update(input);
                return (Guid)input.Id;
            }
        }

        [AbpAuthorize(AppPermissions.Pages_ObeEclUploads_Create)]
        protected virtual async Task<Guid> Create(CreateOrEditObeEclUploadDto input)
        {
            var eclUploadExist = await _obeEclUploadRepository.FirstOrDefaultAsync(x => x.DocType == input.DocType && x.ObeEclId == input.EclId);

            if (eclUploadExist == null)
            {
                var investmentEclUpload = ObjectMapper.Map<ObeEclUpload>(input);

                var id = await _obeEclUploadRepository.InsertAndGetIdAsync(investmentEclUpload);
                return id;
            }
            else
            {
                throw new UserFriendlyException(L("UploadRecordExists"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_ObeEclUploads_Edit)]
        protected virtual async Task Update(CreateOrEditObeEclUploadDto input)
        {
            var obeEclUpload = await _obeEclUploadRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, obeEclUpload);
        }

        [AbpAuthorize(AppPermissions.Pages_ObeEclUploads_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _obeEclUploadRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_ObeEclUploads)]
        public async Task<PagedResultDto<ObeEclUploadObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_obeEclRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.TenantId.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ObeEclUploadObeEclLookupTableDto>();
            foreach (var obeEcl in obeEclList)
            {
                lookupTableDtoList.Add(new ObeEclUploadObeEclLookupTableDto
                {
                    Id = obeEcl.Id.ToString(),
                    DisplayName = obeEcl.TenantId?.ToString()
                });
            }

            return new PagedResultDto<ObeEclUploadObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}