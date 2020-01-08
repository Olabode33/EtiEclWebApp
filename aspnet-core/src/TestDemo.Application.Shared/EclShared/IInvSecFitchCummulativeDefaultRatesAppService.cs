using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclShared
{
    public interface IInvSecFitchCummulativeDefaultRatesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvSecFitchCummulativeDefaultRateForViewDto>> GetAll(GetAllInvSecFitchCummulativeDefaultRatesInput input);

		Task<GetInvSecFitchCummulativeDefaultRateForEditOutput> GetInvSecFitchCummulativeDefaultRateForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditInvSecFitchCummulativeDefaultRateDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}