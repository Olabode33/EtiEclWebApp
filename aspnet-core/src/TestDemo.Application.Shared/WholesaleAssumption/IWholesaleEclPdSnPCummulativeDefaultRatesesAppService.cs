using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.Dto;
using TestDemo.EclShared.Dtos;
using System.Collections.Generic;

namespace TestDemo.WholesaleAssumption
{
    public interface IWholesaleEclPdSnPCummulativeDefaultRatesesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclPdSnPCummulativeDefaultRatesForViewDto>> GetAll(GetAllWholesaleEclPdSnPCummulativeDefaultRatesesInput input);

		Task<GetWholesaleEclPdSnPCummulativeDefaultRatesForEditOutput> GetWholesaleEclPdSnPCummulativeDefaultRatesForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesDto input);

		Task Delete(EntityDto<Guid> input);

        Task<List<PdInputSnPCummulativeDefaultRateDto>> GetListForEclView(EntityDto<Guid> input);
    }
}