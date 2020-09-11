using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.LoanImpairmentScenarios.Dtos;
using TestDemo.Dto;


namespace TestDemo.LoanImpairmentScenarios
{
    public interface ILoanImpairmentScenariosAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLoanImpairmentScenarioForViewDto>> GetAll(GetAllLoanImpairmentScenariosInput input);

        Task<GetLoanImpairmentScenarioForViewDto> GetLoanImpairmentScenarioForView(Guid id);

		Task<GetLoanImpairmentScenarioForEditOutput> GetLoanImpairmentScenarioForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditLoanImpairmentScenarioDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}