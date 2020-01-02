using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;
using System.Collections.Generic;

namespace TestDemo.RetailAssumption
{
    public interface IRetailEclPdAssumptionNplIndexesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclPdAssumptionNplIndexForViewDto>> GetAll(GetAllRetailEclPdAssumptionNplIndexesInput input);

        Task<List<EclShared.Dtos.PdInputAssumptionNplIndexDto>> GetListForEclView(EntityDto<Guid> input);


        Task<GetRetailEclPdAssumptionNplIndexForEditOutput> GetRetailEclPdAssumptionNplIndexForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclPdAssumptionNplIndexDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclPdAssumptionNplIndexRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}