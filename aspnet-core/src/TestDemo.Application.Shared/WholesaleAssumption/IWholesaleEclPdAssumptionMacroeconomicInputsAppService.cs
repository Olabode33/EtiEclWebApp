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
    public interface IWholesaleEclPdAssumptionMacroeconomicInputsAppService : IApplicationService 
    {
		Task<List<PdInputAssumptionMacroeconomicInputDto>> GetListForEclView(EntityDto<Guid> input);

		Task<PagedResultDto<GetWholesaleEclPdAssumptionMacroeconomicInputForViewDto>> GetAll(GetAllWholesaleEclPdAssumptionMacroeconomicInputsInput input);

		Task<GetWholesaleEclPdAssumptionMacroeconomicInputForEditOutput> GetWholesaleEclPdAssumptionMacroeconomicInputForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclPdAssumptionMacroeconomicInputDto input);

		Task Delete(EntityDto<Guid> input);
    }
}