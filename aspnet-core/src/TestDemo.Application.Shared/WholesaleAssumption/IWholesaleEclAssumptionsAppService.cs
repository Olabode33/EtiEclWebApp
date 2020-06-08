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
    public interface IWholesaleEclAssumptionsAppService : IApplicationService 
    {
		Task<List<AssumptionDto>> GetListForEclView(EntityDto<Guid> input);

		Task<PagedResultDto<GetWholesaleEclAssumptionForViewDto>> GetAll(GetAllWholesaleEclAssumptionsInput input);

		Task<GetWholesaleEclAssumptionForEditOutput> GetWholesaleEclAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclAssumptionDto input);

		Task Delete(EntityDto<Guid> input);
    }
}