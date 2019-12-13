using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclShared
{
    public interface IPdInputAssumptionNplIndexesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPdInputAssumptionNplIndexForViewDto>> GetAll(GetAllPdInputAssumptionNplIndexesInput input);

		Task<GetPdInputAssumptionNplIndexForEditOutput> GetPdInputAssumptionNplIndexForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPdInputAssumptionNplIndexDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}