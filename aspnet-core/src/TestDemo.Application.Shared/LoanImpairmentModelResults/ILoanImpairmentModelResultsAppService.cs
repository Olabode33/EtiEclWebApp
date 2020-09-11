using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.LoanImpairmentModelResults.Dtos;
using TestDemo.Dto;


namespace TestDemo.LoanImpairmentModelResults
{
    public interface ILoanImpairmentModelResultsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLoanImpairmentModelResultForViewDto>> GetAll(GetAllLoanImpairmentModelResultsInput input);

        Task<GetLoanImpairmentModelResultForViewDto> GetLoanImpairmentModelResultForView(Guid id);

		Task<GetLoanImpairmentModelResultForEditOutput> GetLoanImpairmentModelResultForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditLoanImpairmentModelResultDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}