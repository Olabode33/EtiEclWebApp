using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeAssumption
{
    public interface IObeEclPdAssumption12MonthsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclPdAssumption12MonthForViewDto>> GetAll(GetAllObeEclPdAssumption12MonthsInput input);

		Task<GetObeEclPdAssumption12MonthForEditOutput> GetObeEclPdAssumption12MonthForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclPdAssumption12MonthDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclPdAssumption12MonthObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}