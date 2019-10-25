using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailAssumption
{
    public interface IRetailEclAssumptionApprovalsesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclAssumptionApprovalsForViewDto>> GetAll(GetAllRetailEclAssumptionApprovalsesInput input);

		Task<GetRetailEclAssumptionApprovalsForEditOutput> GetRetailEclAssumptionApprovalsForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclAssumptionApprovalsDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclAssumptionApprovalsUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<RetailEclAssumptionApprovalsRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}