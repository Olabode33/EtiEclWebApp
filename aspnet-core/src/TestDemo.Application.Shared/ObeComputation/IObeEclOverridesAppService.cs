using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;
using TestDemo.Dto.Overrides;
using TestDemo.InvestmentComputation.Dtos;

namespace TestDemo.ObeComputation
{
    public interface IObeEclOverridesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetEclOverrideForViewDto>> GetAll(GetAllEclOverrideInput input);

		Task<GetObeEclOverrideForEditOutput> GetObeEclOverrideForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditEclOverrideDto input);

		Task Delete(EntityDto<Guid> input);
    }
}