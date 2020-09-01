using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.IVModels.Dtos;
using TestDemo.Dto;


namespace TestDemo.IVModels
{
    public interface IHoldCoRegistersAppService : IApplicationService 
    {
        Task<PagedResultDto<GetHoldCoRegisterForViewDto>> GetAll(GetAllHoldCoRegistersInput input);

        Task<GetHoldCoRegisterForViewDto> GetHoldCoRegisterForView(Guid id);

		Task<GetHoldCoRegisterForEditOutput> GetHoldCoRegisterForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditHoldCoRegisterDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}