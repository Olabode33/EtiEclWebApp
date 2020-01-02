using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;
using System.Collections.Generic;

namespace TestDemo.RetailAssumption
{
    public interface IRetailEclPdSnPCummulativeDefaultRatesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclPdSnPCummulativeDefaultRateForViewDto>> GetAll(GetAllRetailEclPdSnPCummulativeDefaultRatesInput input);

        Task<List<EclShared.Dtos.PdInputSnPCummulativeDefaultRateDto>> GetListForEclView(EntityDto<Guid> input);


        Task<GetRetailEclPdSnPCummulativeDefaultRateForEditOutput> GetRetailEclPdSnPCummulativeDefaultRateForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclPdSnPCummulativeDefaultRateDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclPdSnPCummulativeDefaultRateRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}