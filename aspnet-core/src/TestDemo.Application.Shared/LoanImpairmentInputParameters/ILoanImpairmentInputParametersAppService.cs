using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.LoanImpairmentInputParameters.Dtos;
using TestDemo.Dto;


namespace TestDemo.LoanImpairmentInputParameters
{
    public interface ILoanImpairmentInputParametersAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLoanImpairmentInputParameterForViewDto>> GetAll(GetAllLoanImpairmentInputParametersInput input);

        Task<GetLoanImpairmentInputParameterForViewDto> GetLoanImpairmentInputParameterForView(Guid id);

		Task<GetLoanImpairmentInputParameterForEditOutput> GetLoanImpairmentInputParameterForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditLoanImpairmentInputParameterDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}