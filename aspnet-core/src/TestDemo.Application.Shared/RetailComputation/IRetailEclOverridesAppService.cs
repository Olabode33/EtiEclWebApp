using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;
using TestDemo.InvestmentComputation.Dtos;
using TestDemo.Dto.Overrides;

namespace TestDemo.RetailComputation
{
    public interface IRetailEclOverridesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetEclOverrideForViewDto>> GetAll(GetAllEclOverrideInput input);

		Task<GetRetailEclOverrideForEditOutput> GetRetailEclOverrideForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditEclOverrideNewDto input);

		Task Delete(EntityDto<Guid> input);
    }
}