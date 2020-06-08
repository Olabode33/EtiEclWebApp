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
    public interface IWholesaleEadInputAssumptionsAppService : IApplicationService 
    {
		Task<List<EadInputAssumptionDto>> GetListForEclView(EntityDto<Guid> input);

		Task<PagedResultDto<GetWholesaleEadInputAssumptionForViewDto>> GetAll(GetAllWholesaleEadInputAssumptionsInput input);

		Task<GetWholesaleEadInputAssumptionForEditOutput> GetWholesaleEadInputAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEadInputAssumptionDto input);

		Task Delete(EntityDto<Guid> input);
    }
}