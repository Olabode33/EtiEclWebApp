using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclShared
{
    public interface IPdInputSnPCummulativeDefaultRatesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPdInputSnPCummulativeDefaultRateForViewDto>> GetAll(GetAllPdInputSnPCummulativeDefaultRatesInput input);

		Task<GetPdInputSnPCummulativeDefaultRateForEditOutput> GetPdInputSnPCummulativeDefaultRateForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPdInputSnPCummulativeDefaultRateDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}