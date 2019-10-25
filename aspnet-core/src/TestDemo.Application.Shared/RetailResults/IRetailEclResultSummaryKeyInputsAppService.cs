using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailResults.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailResults
{
    public interface IRetailEclResultSummaryKeyInputsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclResultSummaryKeyInputForViewDto>> GetAll(GetAllRetailEclResultSummaryKeyInputsInput input);

		Task<GetRetailEclResultSummaryKeyInputForEditOutput> GetRetailEclResultSummaryKeyInputForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclResultSummaryKeyInputDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclResultSummaryKeyInputRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}