using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.LoanImpairmentKeyParameters.Dtos;
using TestDemo.Dto;


namespace TestDemo.LoanImpairmentKeyParameters
{
    public interface ILoanImpairmentKeyParametersAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLoanImpairmentKeyParameterForViewDto>> GetAll(GetAllLoanImpairmentKeyParametersInput input);

        Task<GetLoanImpairmentKeyParameterForViewDto> GetLoanImpairmentKeyParameterForView(Guid id);

		Task<GetLoanImpairmentKeyParameterForEditOutput> GetLoanImpairmentKeyParameterForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditLoanImpairmentKeyParameterDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}