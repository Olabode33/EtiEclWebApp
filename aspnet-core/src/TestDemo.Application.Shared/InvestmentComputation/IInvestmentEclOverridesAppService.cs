using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.InvestmentComputation.Dtos;
using TestDemo.Dto;
using TestDemo.Dto.Overrides;

namespace TestDemo.InvestmentComputation
{
    public interface IInvestmentEclOverridesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetEclOverrideForViewDto>> GetAll(GetAllEclOverrideInput input);

		Task<GetInvestmentEclOverrideForEditOutput> GetInvestmentEclOverrideForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditEclOverrideDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<InvestmentEclOverrideInvestmentEclSicrLookupTableDto>> GetAllInvestmentEclSicrForLookupTable(GetAllForLookupTableInput input);
		
    }
}