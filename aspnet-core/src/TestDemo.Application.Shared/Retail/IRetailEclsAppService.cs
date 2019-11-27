using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.Retail.Dtos;
using TestDemo.Dto;
using TestDemo.RetailAssumption.Dtos;

namespace TestDemo.Retail
{
    public interface IRetailEclsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclForViewDto>> GetAll(GetAllRetailEclsInput input);

		Task<GetRetailEclForEditOutput> GetRetailEclForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclDto input);

		Task Delete(EntityDto<Guid> input);

        Task CreateEclAndAssumption(CreateRetailEclAndAssumptions input);

        //Task<PagedResultDto<RetailEclUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);

    }
}