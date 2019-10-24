using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesaleEclSicrsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclSicrForViewDto>> GetAll(GetAllWholesaleEclSicrsInput input);

		Task<GetWholesaleEclSicrForEditOutput> GetWholesaleEclSicrForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclSicrDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclSicrWholesaleEclDataLoanBookLookupTableDto>> GetAllWholesaleEclDataLoanBookForLookupTable(GetAllForLookupTableInput input);
		
    }
}