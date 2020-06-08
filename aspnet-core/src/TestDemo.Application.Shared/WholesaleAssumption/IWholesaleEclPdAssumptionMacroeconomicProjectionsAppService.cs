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
    public interface IWholesaleEclPdAssumptionMacroeconomicProjectionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclPdAssumptionMacroeconomicProjectionForViewDto>> GetAll(GetAllWholesaleEclPdAssumptionMacroeconomicProjectionsInput input);

		Task<GetWholesaleEclPdAssumptionMacroeconomicProjectionForEditOutput> GetWholesaleEclPdAssumptionMacroeconomicProjectionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclPdAssumptionMacroeconomicProjectionDto input);

		Task Delete(EntityDto<Guid> input);

        Task<List<PdInputAssumptionMacroeconomicProjectionDto>> GetListForEclView(EntityDto<Guid> input);
    }
}