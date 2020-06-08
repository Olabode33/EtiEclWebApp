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
    public interface IWholesaleEclPdAssumptionNplIndexesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclPdAssumptionNplIndexForViewDto>> GetAll(GetAllWholesaleEclPdAssumptionNplIndexesInput input);

		Task<GetWholesaleEclPdAssumptionNplIndexForEditOutput> GetWholesaleEclPdAssumptionNplIndexForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclPdAssumptionNplIndexDto input);

		Task Delete(EntityDto<Guid> input);
        Task<List<PdInputAssumptionNplIndexDto>> GetListForEclView(EntityDto<Guid> input);
    }
}