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
    public interface IWholesaleEclLgdAssumptionsAppService : IApplicationService 
    {
		Task<List<LgdAssumptionDto>> GetListForEclView(EntityDto<Guid> input);

		Task<PagedResultDto<GetWholesaleEclLgdAssumptionForViewDto>> GetAll(GetAllWholesaleEclLgdAssumptionsInput input);

		Task<GetWholesaleEclLgdAssumptionForEditOutput> GetWholesaleEclLgdAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclLgdAssumptionDto input);

		Task Delete(EntityDto<Guid> input);
    }
}