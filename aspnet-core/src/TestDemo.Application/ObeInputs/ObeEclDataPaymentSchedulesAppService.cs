using TestDemo.ObeInputs;


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
	[AbpAuthorize(AppPermissions.Pages_ObeEclDataPaymentSchedules)]
    public class ObeEclDataPaymentSchedulesAppService : TestDemoAppServiceBase, IObeEclDataPaymentSchedulesAppService
    {
		 private readonly IRepository<ObeEclDataPaymentSchedule, Guid> _obeEclDataPaymentScheduleRepository;
		 private readonly IRepository<ObeEclUpload,Guid> _lookup_obeEclUploadRepository;
		 

		  public ObeEclDataPaymentSchedulesAppService(IRepository<ObeEclDataPaymentSchedule, Guid> obeEclDataPaymentScheduleRepository , IRepository<ObeEclUpload, Guid> lookup_obeEclUploadRepository) 
		  {
			_obeEclDataPaymentScheduleRepository = obeEclDataPaymentScheduleRepository;
			_lookup_obeEclUploadRepository = lookup_obeEclUploadRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclDataPaymentScheduleForViewDto>> GetAll(GetAllObeEclDataPaymentSchedulesInput input)
         {
			
			var filteredObeEclDataPaymentSchedules = _obeEclDataPaymentScheduleRepository.GetAll()
						//.Include( e => e.ObeEclUploadFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ContractRefNo.Contains(input.Filter) || e.Component.Contains(input.Filter) || e.Frequency.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContractRefNoFilter),  e => e.ContractRefNo.ToLower() == input.ContractRefNoFilter.ToLower().Trim())
						.WhereIf(input.MinStartDateFilter != null, e => e.StartDate >= input.MinStartDateFilter)
						.WhereIf(input.MaxStartDateFilter != null, e => e.StartDate <= input.MaxStartDateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ComponentFilter),  e => e.Component.ToLower() == input.ComponentFilter.ToLower().Trim())
						.WhereIf(input.MinNoOfSchedulesFilter != null, e => e.NoOfSchedules >= input.MinNoOfSchedulesFilter)
						.WhereIf(input.MaxNoOfSchedulesFilter != null, e => e.NoOfSchedules <= input.MaxNoOfSchedulesFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.FrequencyFilter),  e => e.Frequency.ToLower() == input.FrequencyFilter.ToLower().Trim())
						.WhereIf(input.MinAmountFilter != null, e => e.Amount >= input.MinAmountFilter)
						.WhereIf(input.MaxAmountFilter != null, e => e.Amount <= input.MaxAmountFilter);

			var pagedAndFilteredObeEclDataPaymentSchedules = filteredObeEclDataPaymentSchedules
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclDataPaymentSchedules = from o in pagedAndFilteredObeEclDataPaymentSchedules
                         join o1 in _lookup_obeEclUploadRepository.GetAll() on o.ObeEclUploadId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclDataPaymentScheduleForViewDto() {
							ObeEclDataPaymentSchedule = new ObeEclDataPaymentScheduleDto
							{
                                ContractRefNo = o.ContractRefNo,
                                StartDate = o.StartDate,
                                Component = o.Component,
                                NoOfSchedules = o.NoOfSchedules,
                                Frequency = o.Frequency,
                                Amount = o.Amount,
                                Id = o.Id
							},
                         	ObeEclUploadTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEclDataPaymentSchedules.CountAsync();

            return new PagedResultDto<GetObeEclDataPaymentScheduleForViewDto>(
                totalCount,
                await obeEclDataPaymentSchedules.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEclDataPaymentSchedules_Edit)]
		 public async Task<GetObeEclDataPaymentScheduleForEditOutput> GetObeEclDataPaymentScheduleForEdit(EntityDto<Guid> input)
         {
            var obeEclDataPaymentSchedule = await _obeEclDataPaymentScheduleRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclDataPaymentScheduleForEditOutput {ObeEclDataPaymentSchedule = ObjectMapper.Map<CreateOrEditObeEclDataPaymentScheduleDto>(obeEclDataPaymentSchedule)};

		    if (output.ObeEclDataPaymentSchedule.ObeEclUploadId != null)
            {
                var _lookupObeEclUpload = await _lookup_obeEclUploadRepository.FirstOrDefaultAsync((Guid)output.ObeEclDataPaymentSchedule.ObeEclUploadId);
                output.ObeEclUploadTenantId = _lookupObeEclUpload.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclDataPaymentScheduleDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclDataPaymentSchedules_Create)]
		 protected virtual async Task Create(CreateOrEditObeEclDataPaymentScheduleDto input)
         {
            var obeEclDataPaymentSchedule = ObjectMapper.Map<ObeEclDataPaymentSchedule>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEclDataPaymentSchedule.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclDataPaymentScheduleRepository.InsertAsync(obeEclDataPaymentSchedule);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclDataPaymentSchedules_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEclDataPaymentScheduleDto input)
         {
            var obeEclDataPaymentSchedule = await _obeEclDataPaymentScheduleRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclDataPaymentSchedule);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclDataPaymentSchedules_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclDataPaymentScheduleRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEclDataPaymentSchedules)]
         public async Task<PagedResultDto<ObeEclDataPaymentScheduleObeEclUploadLookupTableDto>> GetAllObeEclUploadForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclUploadRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclUploadList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclDataPaymentScheduleObeEclUploadLookupTableDto>();
			foreach(var obeEclUpload in obeEclUploadList){
				lookupTableDtoList.Add(new ObeEclDataPaymentScheduleObeEclUploadLookupTableDto
				{
					Id = obeEclUpload.Id.ToString(),
					DisplayName = obeEclUpload.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclDataPaymentScheduleObeEclUploadLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}