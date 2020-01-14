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

namespace TestDemo.WholesaleInputs
{
	[AbpAuthorize(AppPermissions.Pages_WholesaleEclUploads)]
    public class WholesaleEclUploadsAppService : TestDemoAppServiceBase, IWholesaleEclUploadsAppService
    {
		 private readonly IRepository<WholesaleEclUpload, Guid> _wholesaleEclUploadRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleEclUploadsAppService(IRepository<WholesaleEclUpload, Guid> wholesaleEclUploadRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleEclUploadRepository = wholesaleEclUploadRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclUploadForViewDto>> GetAll(GetAllWholesaleEclUploadsInput input)
         {
			var docTypeFilter = (UploadDocTypeEnum) input.DocTypeFilter;
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredWholesaleEclUploads = _wholesaleEclUploadRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.UploadComment.Contains(input.Filter))
						.WhereIf(input.DocTypeFilter > -1, e => e.DocType == docTypeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UploadCommentFilter),  e => e.UploadComment.ToLower() == input.UploadCommentFilter.ToLower().Trim())
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter);

			var pagedAndFilteredWholesaleEclUploads = filteredWholesaleEclUploads
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclUploads = from o in pagedAndFilteredWholesaleEclUploads
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclUploadForViewDto() {
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
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclUploads_Edit)]
		 public async Task<GetWholesaleEclUploadForEditOutput> GetWholesaleEclUploadForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclUpload = await _wholesaleEclUploadRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclUploadForEditOutput {WholesaleEclUpload = ObjectMapper.Map<CreateOrEditWholesaleEclUploadDto>(wholesaleEclUpload)};

		    if (output.WholesaleEclUpload.EclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclUpload.EclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclUploadDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclUploads_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEclUploadDto input)
         {
            var wholesaleEclUpload = ObjectMapper.Map<WholesaleEclUpload>(input);

			
			if (AbpSession.TenantId != null)
			{
				wholesaleEclUpload.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _wholesaleEclUploadRepository.InsertAsync(wholesaleEclUpload);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclUploads_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEclUploadDto input)
         {
            var wholesaleEclUpload = await _wholesaleEclUploadRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclUpload);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclUploads_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclUploadRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEclUploads)]
         public async Task<PagedResultDto<WholesaleEclUploadWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclUploadWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
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