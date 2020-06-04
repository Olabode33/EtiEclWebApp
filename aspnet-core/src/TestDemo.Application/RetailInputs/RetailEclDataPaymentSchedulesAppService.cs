using TestDemo.RetailInputs;


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
	[AbpAuthorize(AppPermissions.Pages_RetailEclDataPaymentSchedules)]
    public class RetailEclDataPaymentSchedulesAppService : TestDemoAppServiceBase, IRetailEclDataPaymentSchedulesAppService
    {
		 private readonly IRepository<RetailEclDataPaymentSchedule, Guid> _retailEclDataPaymentScheduleRepository;
		 private readonly IRepository<RetailEclUpload,Guid> _lookup_retailEclUploadRepository;
		 

		  public RetailEclDataPaymentSchedulesAppService(IRepository<RetailEclDataPaymentSchedule, Guid> retailEclDataPaymentScheduleRepository , IRepository<RetailEclUpload, Guid> lookup_retailEclUploadRepository) 
		  {
			_retailEclDataPaymentScheduleRepository = retailEclDataPaymentScheduleRepository;
			_lookup_retailEclUploadRepository = lookup_retailEclUploadRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclDataPaymentScheduleForViewDto>> GetAll(GetAllRetailEclDataPaymentSchedulesInput input)
         {
			
			var filteredRetailEclDataPaymentSchedules = _retailEclDataPaymentScheduleRepository.GetAll()
						//.Include( e => e.RetailEclUploadFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ContractRefNo.Contains(input.Filter) || e.Component.Contains(input.Filter) || e.Frequency.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContractRefNoFilter),  e => e.ContractRefNo.ToLower() == input.ContractRefNoFilter.ToLower().Trim());

			var pagedAndFilteredRetailEclDataPaymentSchedules = filteredRetailEclDataPaymentSchedules
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclDataPaymentSchedules = from o in pagedAndFilteredRetailEclDataPaymentSchedules
                         join o1 in _lookup_retailEclUploadRepository.GetAll() on o.RetailEclUploadId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEclDataPaymentScheduleForViewDto() {
							EclDataPaymentSchedule = new RetailEclDataPaymentScheduleDto
							{
                                ContractRefNo = o.ContractRefNo,
                                StartDate = o.StartDate,
                                Component = o.Component,
                                NoOfSchedules = o.NoOfSchedules,
                                Frequency = o.Frequency,
                                Amount = o.Amount,
                                Id = o.Id
							},
                         	RetailEclUploadTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailEclDataPaymentSchedules.CountAsync();

            return new PagedResultDto<GetRetailEclDataPaymentScheduleForViewDto>(
                totalCount,
                await retailEclDataPaymentSchedules.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclDataPaymentSchedules_Edit)]
		 public async Task<GetRetailEclDataPaymentScheduleForEditOutput> GetRetailEclDataPaymentScheduleForEdit(EntityDto<Guid> input)
         {
            var retailEclDataPaymentSchedule = await _retailEclDataPaymentScheduleRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclDataPaymentScheduleForEditOutput {RetailEclDataPaymentSchedule = ObjectMapper.Map<CreateOrEditRetailEclDataPaymentScheduleDto>(retailEclDataPaymentSchedule)};

		    if (output.RetailEclDataPaymentSchedule.RetailEclUploadId != null)
            {
                var _lookupRetailEclUpload = await _lookup_retailEclUploadRepository.FirstOrDefaultAsync((Guid)output.RetailEclDataPaymentSchedule.RetailEclUploadId);
                output.RetailEclUploadTenantId = _lookupRetailEclUpload.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclDataPaymentScheduleDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclDataPaymentSchedules_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclDataPaymentScheduleDto input)
         {
            var retailEclDataPaymentSchedule = ObjectMapper.Map<RetailEclDataPaymentSchedule>(input);

			
			if (AbpSession.TenantId != null)
			{
				retailEclDataPaymentSchedule.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _retailEclDataPaymentScheduleRepository.InsertAsync(retailEclDataPaymentSchedule);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclDataPaymentSchedules_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclDataPaymentScheduleDto input)
         {
            var retailEclDataPaymentSchedule = await _retailEclDataPaymentScheduleRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclDataPaymentSchedule);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclDataPaymentSchedules_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclDataPaymentScheduleRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclDataPaymentSchedules)]
         public async Task<PagedResultDto<RetailEclDataPaymentScheduleRetailEclUploadLookupTableDto>> GetAllRetailEclUploadForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclUploadRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclUploadList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclDataPaymentScheduleRetailEclUploadLookupTableDto>();
			foreach(var retailEclUpload in retailEclUploadList){
				lookupTableDtoList.Add(new RetailEclDataPaymentScheduleRetailEclUploadLookupTableDto
				{
					Id = retailEclUpload.Id.ToString(),
					DisplayName = retailEclUpload.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEclDataPaymentScheduleRetailEclUploadLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}