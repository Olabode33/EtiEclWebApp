using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleInputs.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleInputs
{
    public interface IWholesaleEclDataLoanBooksAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclDataLoanBookForViewDto>> GetAll(GetAllWholesaleEclDataLoanBooksInput input);

		Task<GetWholesaleEclDataLoanBookForEditOutput> GetWholesaleEclDataLoanBookForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclDataLoanBookDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclDataLoanBookWholesaleEclUploadLookupTableDto>> GetAllWholesaleEclUploadForLookupTable(GetAllForLookupTableInput input);
		
    }
}