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

namespace TestDemo.ObeInputs
{
	[AbpAuthorize(AppPermissions.Pages_ObeEclUploads)]
    public class ObeEclUploadsAppService : TestDemoAppServiceBase, IObeEclUploadsAppService
    {
		 private readonly IRepository<ObeEclUpload, Guid> _obeEclUploadRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeEclUploadsAppService(IRepository<ObeEclUpload, Guid> obeEclUploadRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeEclUploadRepository = obeEclUploadRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclUploadForViewDto>> GetAll(GetAllObeEclUploadsInput input)
         {
			var docTypeFilter = (UploadDocTypeEnum) input.DocTypeFilter;
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredObeEclUploads = _obeEclUploadRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.UploadComment.Contains(input.Filter))
						.WhereIf(input.DocTypeFilter > -1, e => e.DocType == docTypeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UploadCommentFilter),  e => e.UploadComment.ToLower() == input.UploadCommentFilter.ToLower().Trim())
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclTenantIdFilter), e => e.ObeEclFk != null && e.ObeEclFk.TenantId.ToLower() == input.ObeEclTenantIdFilter.ToLower().Trim());

			var pagedAndFilteredObeEclUploads = filteredObeEclUploads
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclUploads = from o in pagedAndFilteredObeEclUploads
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclUploadForViewDto() {
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
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEclUploads_Edit)]
		 public async Task<GetObeEclUploadForEditOutput> GetObeEclUploadForEdit(EntityDto<Guid> input)
         {
            var obeEclUpload = await _obeEclUploadRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclUploadForEditOutput {ObeEclUpload = ObjectMapper.Map<CreateOrEditObeEclUploadDto>(obeEclUpload)};

		    if (output.ObeEclUpload.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclUpload.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclUploadDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclUploads_Create)]
		 protected virtual async Task Create(CreateOrEditObeEclUploadDto input)
         {
            var obeEclUpload = ObjectMapper.Map<ObeEclUpload>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEclUpload.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclUploadRepository.InsertAsync(obeEclUpload);
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
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclUploadObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
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