using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.Investment.Dtos;
using TestDemo.Dto;

namespace TestDemo.Investment
{
    public interface IInvestmentEclsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvestmentEclForViewDto>> GetAll(GetAllInvestmentEclsInput input);
        
		Task CreateOrEdit(CreateOrEditInvestmentEclDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<InvestmentEclUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
    }
}