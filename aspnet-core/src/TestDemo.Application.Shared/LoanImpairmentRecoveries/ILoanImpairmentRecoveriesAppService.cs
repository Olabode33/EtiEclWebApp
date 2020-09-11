using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.LoanImpairmentRecoveries.Dtos;
using TestDemo.Dto;


namespace TestDemo.LoanImpairmentRecoveries
{
    public interface ILoanImpairmentRecoveriesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLoanImpairmentRecoveryForViewDto>> GetAll(GetAllLoanImpairmentRecoveriesInput input);

        Task<GetLoanImpairmentRecoveryForViewDto> GetLoanImpairmentRecoveryForView(Guid id);

		Task<GetLoanImpairmentRecoveryForEditOutput> GetLoanImpairmentRecoveryForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditLoanImpairmentRecoveryDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}