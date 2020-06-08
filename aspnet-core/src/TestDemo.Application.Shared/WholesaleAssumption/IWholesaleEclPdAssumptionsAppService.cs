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
    public interface IWholesaleEclPdAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclPdAssumptionForViewDto>> GetAll(GetAllWholesaleEclPdAssumptionsInput input);

		Task<GetWholesaleEclPdAssumptionForEditOutput> GetWholesaleEclPdAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclPdAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

        Task<List<PdInputAssumptionDto>> GetListForEclView(EntityDto<Guid> input);
    }
}