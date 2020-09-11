using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.LoanImpairmentHaircuts.Dtos;
using TestDemo.Dto;


namespace TestDemo.LoanImpairmentHaircuts
{
    public interface ILoanImpairmentHaircutsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLoanImpairmentHaircutForViewDto>> GetAll(GetAllLoanImpairmentHaircutsInput input);

        Task<GetLoanImpairmentHaircutForViewDto> GetLoanImpairmentHaircutForView(Guid id);

		Task<GetLoanImpairmentHaircutForEditOutput> GetLoanImpairmentHaircutForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditLoanImpairmentHaircutDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}