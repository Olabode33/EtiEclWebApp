using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleAssumption
{
    public interface IWholesaleEclPdAssumption12MonthsesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclPdAssumption12MonthsForViewDto>> GetAll(GetAllWholesaleEclPdAssumption12MonthsesInput input);

		Task<GetWholesaleEclPdAssumption12MonthsForEditOutput> GetWholesaleEclPdAssumption12MonthsForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclPdAssumption12MonthsDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclPdAssumption12MonthsWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}