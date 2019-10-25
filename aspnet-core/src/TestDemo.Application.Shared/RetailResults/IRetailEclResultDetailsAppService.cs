using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailResults.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailResults
{
    public interface IRetailEclResultDetailsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclResultDetailForViewDto>> GetAll(GetAllRetailEclResultDetailsInput input);

		Task<GetRetailEclResultDetailForEditOutput> GetRetailEclResultDetailForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclResultDetailDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclResultDetailRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<RetailEclResultDetailRetailEclDataLoanBookLookupTableDto>> GetAllRetailEclDataLoanBookForLookupTable(GetAllForLookupTableInput input);
		
    }
}