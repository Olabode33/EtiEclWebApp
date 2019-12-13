using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclShared
{
    public interface IPdInputAssumptionNonInternalModelsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPdInputAssumptionNonInternalModelForViewDto>> GetAll(GetAllPdInputAssumptionNonInternalModelsInput input);

		Task<GetPdInputAssumptionNonInternalModelForEditOutput> GetPdInputAssumptionNonInternalModelForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPdInputAssumptionNonInternalModelDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}