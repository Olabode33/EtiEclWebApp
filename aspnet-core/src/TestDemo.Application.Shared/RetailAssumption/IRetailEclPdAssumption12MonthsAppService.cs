using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailAssumption
{
    public interface IRetailEclPdAssumption12MonthsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclPdAssumption12MonthForViewDto>> GetAll(GetAllRetailEclPdAssumption12MonthsInput input);

		Task<GetRetailEclPdAssumption12MonthForEditOutput> GetRetailEclPdAssumption12MonthForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclPdAssumption12MonthDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclPdAssumption12MonthRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}