using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.HoldCoInterCompanyResults.Dtos;
using TestDemo.Dto;


namespace TestDemo.HoldCoInterCompanyResults
{
    public interface IHoldCoInterCompanyResultsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetHoldCoInterCompanyResultForViewDto>> GetAll(GetAllHoldCoInterCompanyResultsInput input);

        Task<GetHoldCoInterCompanyResultForViewDto> GetHoldCoInterCompanyResultForView(Guid id);

		Task<GetHoldCoInterCompanyResultForEditOutput> GetHoldCoInterCompanyResultForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditHoldCoInterCompanyResultDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}