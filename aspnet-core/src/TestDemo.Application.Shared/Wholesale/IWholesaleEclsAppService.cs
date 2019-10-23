using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.Wholesale.Dtos;
using TestDemo.Dto;

namespace TestDemo.Wholesale
{
    public interface IWholesaleEclsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclForViewDto>> GetAll(GetAllWholesaleEclsInput input);

		Task<GetWholesaleEclForEditOutput> GetWholesaleEclForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
    }
}