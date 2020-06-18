using TestDemo.Wholesale;

using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.WholesaleInputs.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.Dto.Inputs;
using TestDemo.Authorization.Users;
using Abp.UI;

namespace TestDemo.WholesaleInputs
{
    public class WholesaleEclUploadsAppService : TestDemoAppServiceBase, IWholesaleEclUploadsAppService
    {
        private readonly IRepository<WholesaleEclUpload, Guid> _wholesaleEclUploadRepository;
        private readonly IRepository<WholesaleEcl, Guid> _lookup_wholesaleEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;


        public WholesaleEclUploadsAppService(
            IRepository<WholesaleEclUpload, Guid> wholesaleEclUploadRepository,
            IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository,
            IRepository<User, long> lookup_userRepository)
        {
            _wholesaleEclUploadRepository = wholesaleEclUploadRepository;
            _lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
            _lookup_userRepository = lookup_userRepository;
        }

        public async Task<PagedResultDto<GetWholesaleEclUploadForViewDto>> GetAll(GetAllWholesaleEclUploadsInput input)
        {
            var docTypeFilter = (UploadDocTypeEnum)input.DocTypeFilter;
            var statusFilter = (GeneralStatusEnum)input.StatusFilter;

            var filteredWholesaleEclUploads = _wholesaleEclUploadRepository.GetAll()
                        .Include(e => e.WholesaleEclFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.UploadComment.Contains(input.Filter))
                        .WhereIf(input.DocTypeFilter > -1, e => e.DocType == docTypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UploadCommentFilter), e => e.UploadComment.ToLower() == input.UploadCommentFilter.ToLower().Trim())
                        .WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter);

            var pagedAndFilteredWholesaleEclUploads = filteredWholesaleEclUploads
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var wholesaleEclUploads = from o in pagedAndFilteredWholesaleEclUploads
                                      join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                                      from s1 in j1.DefaultIfEmpty()

                                      select new GetWholesaleEclUploadForViewDto()
                                      {
                                          WholesaleEclUpload = new WholesaleEclUploadDto
                                          {
                                              DocType = o.DocType,
                                              UploadComment = o.UploadComment,
                                              Status = o.Status,
                                              Id = o.Id
                                          },
                                          WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
                                      };

            var totalCount = await filteredWholesaleEclUploads.CountAsync();

            return new PagedResultDto<GetWholesaleEclUploadForViewDto>(
                totalCount,
                await wholesaleEclUploads.ToListAsync()
            );
        }

        public async Task<List<GetEclUploadForViewDto>> GetEclUploads(EntityDto<Guid> input)
        {
            var eclUploads = from o in _wholesaleEclUploadRepository.GetAll().Where(x => x.WholesaleEclId == input.Id)
                             join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
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
                                     EclId = o.WholesaleEclId,
                                     Id = o.Id
                                 },
                                 DateUploaded = o.CreationTime,
                                 UploadedBy = u2 == null ? "" : u2.FullName
                             };

            return await eclUploads.ToListAsync();
        }

        public async Task<GetWholesaleEclUploadForEditOutput> GetWholesaleEclUploadForEdit(EntityDto<Guid> input)
        {
            var wholesaleEclUpload = await _wholesaleEclUploadRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetWholesaleEclUploadForEditOutput { WholesaleEclUpload = ObjectMapper.Map<CreateOrEditWholesaleEclUploadDto>(wholesaleEclUpload) };

            if (output.WholesaleEclUpload.EclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclUpload.EclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }

            return output;
        }

        public async Task<Guid> CreateOrEdit(CreateOrEditWholesaleEclUploadDto input)
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

        protected virtual async Task<Guid> Create(CreateOrEditWholesaleEclUploadDto input)
        {
            var eclUploadExist = await _wholesaleEclUploadRepository.FirstOrDefaultAsync(x => x.DocType == input.DocType && x.WholesaleEclId == input.EclId);

            if (eclUploadExist == null)
            {
                var eclUpload = ObjectMapper.Map<WholesaleEclUpload>(input);


                if (AbpSession.TenantId != null)
                {
                    eclUpload.TenantId = (int?)AbpSession.TenantId;
                }


                var id = await _wholesaleEclUploadRepository.InsertAndGetIdAsync(eclUpload);
                return id;
            }
            else
            {
                throw new UserFriendlyException(L("UploadRecordExists"));
            }
        }

        protected virtual async Task Update(CreateOrEditWholesaleEclUploadDto input)
        {
            var wholesaleEclUpload = await _wholesaleEclUploadRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, wholesaleEclUpload);
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _wholesaleEclUploadRepository.DeleteAsync(input.Id);
        }

        public async Task<PagedResultDto<WholesaleEclUploadWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.TenantId.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<WholesaleEclUploadWholesaleEclLookupTableDto>();
            foreach (var wholesaleEcl in wholesaleEclList)
            {
                lookupTableDtoList.Add(new WholesaleEclUploadWholesaleEclLookupTableDto
                {
                    Id = wholesaleEcl.Id.ToString(),
                    DisplayName = wholesaleEcl.TenantId?.ToString()
                });
            }

            return new PagedResultDto<WholesaleEclUploadWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}