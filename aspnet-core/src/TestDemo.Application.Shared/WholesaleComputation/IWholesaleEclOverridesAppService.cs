using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;
using TestDemo.InvestmentComputation.Dtos;
using TestDemo.Dto.Overrides;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesaleEclOverridesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetEclOverrideForViewDto>> GetAll(GetAllEclOverrideInput input);

		Task<GetWholesaleEclOverrideForEditOutput> GetWholesaleEclOverrideForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditEclOverrideDto input);

		Task Delete(EntityDto<Guid> input);
    }
}