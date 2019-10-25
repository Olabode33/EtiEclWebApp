using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailInputs.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailInputs
{
    public interface IRetailEclDataLoanBooksAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclDataLoanBookForViewDto>> GetAll(GetAllRetailEclDataLoanBooksInput input);

		Task<GetRetailEclDataLoanBookForEditOutput> GetRetailEclDataLoanBookForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclDataLoanBookDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclDataLoanBookRetailEclUploadLookupTableDto>> GetAllRetailEclUploadForLookupTable(GetAllForLookupTableInput input);
		
    }
}