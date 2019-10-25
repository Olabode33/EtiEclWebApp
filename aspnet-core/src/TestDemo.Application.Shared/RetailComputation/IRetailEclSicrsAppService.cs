using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailComputation
{
    public interface IRetailEclSicrsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclSicrForViewDto>> GetAll(GetAllRetailEclSicrsInput input);

		Task<GetRetailEclSicrForEditOutput> GetRetailEclSicrForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclSicrDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclSicrRetailEclDataLoanBookLookupTableDto>> GetAllRetailEclDataLoanBookForLookupTable(GetAllForLookupTableInput input);
		
    }
}