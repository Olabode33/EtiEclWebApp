using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclShared
{
    public interface ILgdAssumptionUnsecuredRecoveriesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLgdAssumptionUnsecuredRecoveryForViewDto>> GetAll(GetAllLgdAssumptionUnsecuredRecoveriesInput input);

		Task<GetLgdAssumptionUnsecuredRecoveryForEditOutput> GetLgdAssumptionUnsecuredRecoveryForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditLgdAssumptionUnsecuredRecoveryDto input);

		Task Delete(EntityDto<Guid> input);

        Task UpdateStatus(UpdateAssumptionStatusDto input);
    }
}