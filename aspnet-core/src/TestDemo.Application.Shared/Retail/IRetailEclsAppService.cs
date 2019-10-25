using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.Retail.Dtos;
using TestDemo.Dto;

namespace TestDemo.Retail
{
    public interface IRetailEclsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclForViewDto>> GetAll(GetAllRetailEclsInput input);

		Task<GetRetailEclForEditOutput> GetRetailEclForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
    }
}