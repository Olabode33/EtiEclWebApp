using TestDemo.WholesaleInputs;


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
	[AbpAuthorize(AppPermissions.Pages_WholesaleEclDataPaymentSchedules)]
    public class WholesaleEclDataPaymentSchedulesAppService : TestDemoAppServiceBase, IWholesaleEclDataPaymentSchedulesAppService
    {
		 private readonly IRepository<WholesaleEclDataPaymentSchedule, Guid> _wholesaleEclDataPaymentScheduleRepository;
		 private readonly IRepository<WholesaleEclUpload,Guid> _lookup_wholesaleEclUploadRepository;
		 

		  public WholesaleEclDataPaymentSchedulesAppService(IRepository<WholesaleEclDataPaymentSchedule, Guid> wholesaleEclDataPaymentScheduleRepository , IRepository<WholesaleEclUpload, Guid> lookup_wholesaleEclUploadRepository) 
		  {
			_wholesaleEclDataPaymentScheduleRepository = wholesaleEclDataPaymentScheduleRepository;
			_lookup_wholesaleEclUploadRepository = lookup_wholesaleEclUploadRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclDataPaymentScheduleForViewDto>> GetAll(GetAllWholesaleEclDataPaymentSchedulesInput input)
         {

            var filteredWholesaleEclDataPaymentSchedules = _wholesaleEclDataPaymentScheduleRepository.GetAll()
                        //.Include( e => e.WholesaleEclUploadFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ContractRefNo.Contains(input.Filter) || e.Component.Contains(input.Filter) || e.Frequency.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ContractRefNoFilter), e => e.ContractRefNo.ToLower() == input.ContractRefNoFilter.ToLower().Trim())
                        .WhereIf(input.MinStartDateFilter != null, e => e.StartDate >= input.MinStartDateFilter)
                        .WhereIf(input.MaxStartDateFilter != null, e => e.StartDate <= input.MaxStartDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ComponentFilter), e => e.Component.ToLower() == input.ComponentFilter.ToLower().Trim())
                        .WhereIf(input.MinNoOfSchedulesFilter != null, e => e.NoOfSchedules >= input.MinNoOfSchedulesFilter)
                        .WhereIf(input.MaxNoOfSchedulesFilter != null, e => e.NoOfSchedules <= input.MaxNoOfSchedulesFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FrequencyFilter), e => e.Frequency.ToLower() == input.FrequencyFilter.ToLower().Trim())
                        .WhereIf(input.MinAmountFilter != null, e => e.Amount >= input.MinAmountFilter)
                        .WhereIf(input.MaxAmountFilter != null, e => e.Amount <= input.MaxAmountFilter);
						//.WhereIf(!string.IsNullOrWhiteSpace(input.WholesaleEclUploadUploadCommentFilter), e => e.WholesaleEclUploadFk != null && e.WholesaleEclUploadFk.UploadComment.ToLower() == input.WholesaleEclUploadUploadCommentFilter.ToLower().Trim());

			var pagedAndFilteredWholesaleEclDataPaymentSchedules = filteredWholesaleEclDataPaymentSchedules
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclDataPaymentSchedules = from o in pagedAndFilteredWholesaleEclDataPaymentSchedules
                         join o1 in _lookup_wholesaleEclUploadRepository.GetAll() on o.WholesaleEclUploadId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclDataPaymentScheduleForViewDto() {
							WholesaleEclDataPaymentSchedule = new WholesaleEclDataPaymentScheduleDto
							{
                                ContractRefNo = o.ContractRefNo,
                                StartDate = o.StartDate,
                                Component = o.Component,
                                NoOfSchedules = o.NoOfSchedules,
                                Frequency = o.Frequency,
                                Amount = o.Amount,
                                Id = o.Id
							},
                         	WholesaleEclUploadUploadComment = s1 == null ? "" : s1.UploadComment.ToString()
						};

            var totalCount = await filteredWholesaleEclDataPaymentSchedules.CountAsync();

            return new PagedResultDto<GetWholesaleEclDataPaymentScheduleForViewDto>(
                totalCount,
                await wholesaleEclDataPaymentSchedules.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclDataPaymentSchedules_Edit)]
		 public async Task<GetWholesaleEclDataPaymentScheduleForEditOutput> GetWholesaleEclDataPaymentScheduleForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclDataPaymentSchedule = await _wholesaleEclDataPaymentScheduleRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclDataPaymentScheduleForEditOutput {WholesaleEclDataPaymentSchedule = ObjectMapper.Map<CreateOrEditWholesaleEclDataPaymentScheduleDto>(wholesaleEclDataPaymentSchedule)};

		    if (output.WholesaleEclDataPaymentSchedule.WholesaleEclUploadId != null)
            {
                var _lookupWholesaleEclUpload = await _lookup_wholesaleEclUploadRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclDataPaymentSchedule.WholesaleEclUploadId);
                output.WholesaleEclUploadUploadComment = _lookupWholesaleEclUpload.UploadComment.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclDataPaymentScheduleDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclDataPaymentSchedules_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEclDataPaymentScheduleDto input)
         {
            var wholesaleEclDataPaymentSchedule = ObjectMapper.Map<WholesaleEclDataPaymentSchedule>(input);

			
			if (AbpSession.TenantId != null)
			{
				wholesaleEclDataPaymentSchedule.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _wholesaleEclDataPaymentScheduleRepository.InsertAsync(wholesaleEclDataPaymentSchedule);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclDataPaymentSchedules_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEclDataPaymentScheduleDto input)
         {
            var wholesaleEclDataPaymentSchedule = await _wholesaleEclDataPaymentScheduleRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclDataPaymentSchedule);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclDataPaymentSchedules_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclDataPaymentScheduleRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEclDataPaymentSchedules)]
         public async Task<PagedResultDto<WholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableDto>> GetAllWholesaleEclUploadForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclUploadRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.UploadComment.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclUploadList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableDto>();
			foreach(var wholesaleEclUpload in wholesaleEclUploadList){
				lookupTableDtoList.Add(new WholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableDto
				{
					Id = wholesaleEclUpload.Id.ToString(),
					DisplayName = wholesaleEclUpload.UploadComment?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}