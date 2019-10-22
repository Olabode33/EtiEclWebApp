using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclShared
{
    public interface IPdInputAssumption12MonthsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPdInputAssumption12MonthForViewDto>> GetAll(GetAllPdInputAssumption12MonthsInput input);

		Task<GetPdInputAssumption12MonthForEditOutput> GetPdInputAssumption12MonthForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPdInputAssumption12MonthDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}