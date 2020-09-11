using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.LoanImpairmentsRegisters.Dtos;
using TestDemo.Dto;


namespace TestDemo.LoanImpairmentsRegisters
{
    public interface ILoanImpairmentRegistersAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLoanImpairmentRegisterForViewDto>> GetAll(GetAllLoanImpairmentRegistersInput input);

        Task<GetLoanImpairmentRegisterForViewDto> GetLoanImpairmentRegisterForView(Guid id);

		Task<GetLoanImpairmentRegisterForEditOutput> GetLoanImpairmentRegisterForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditLoanImpairmentRegisterDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}