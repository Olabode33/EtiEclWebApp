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
    public interface IWholesalePdAssumptionNonInternalModelsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesalePdAssumptionNonInternalModelForViewDto>> GetAll(GetAllWholesalePdAssumptionNonInternalModelsInput input);

		Task<GetWholesalePdAssumptionNonInternalModelForEditOutput> GetWholesalePdAssumptionNonInternalModelForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesalePdAssumptionNonInternalModelDto input);

		Task Delete(EntityDto<Guid> input);

        Task<List<PdInputAssumptionNonInternalModelDto>> GetListForEclView(EntityDto<Guid> input);
    }
}