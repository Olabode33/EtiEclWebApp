using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;
using System.Collections.Generic;
using TestDemo.EclShared.Dtos;
using GetAllForLookupTableInput = TestDemo.RetailAssumption.Dtos.GetAllForLookupTableInput;

namespace TestDemo.RetailAssumption
{
    public interface IRetailEclAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclAssumptionForViewDto>> GetAll(GetAllRetailEclAssumptionsInput input);

		Task<GetRetailEclAssumptionForEditOutput> GetRetailEclAssumptionForEdit(EntityDto<Guid> input);

        Task<List<AssumptionDto>> GetListForEclView(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditRetailEclAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclAssumptionRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}