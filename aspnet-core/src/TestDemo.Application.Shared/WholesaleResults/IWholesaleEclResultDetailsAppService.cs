using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleResults.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleResults
{
    public interface IWholesaleEclResultDetailsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclResultDetailForViewDto>> GetAll(GetAllWholesaleEclResultDetailsInput input);

		Task<GetWholesaleEclResultDetailForEditOutput> GetWholesaleEclResultDetailForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclResultDetailDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclResultDetailWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<WholesaleEclResultDetailWholesaleEclDataLoanBookLookupTableDto>> GetAllWholesaleEclDataLoanBookForLookupTable(GetAllForLookupTableInput input);
		
    }
}