using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ReceivablesRegisters.Dtos;
using TestDemo.Dto;


namespace TestDemo.ReceivablesRegisters
{
    public interface IReceivablesRegistersAppService : IApplicationService 
    {
        Task<PagedResultDto<GetReceivablesRegisterForViewDto>> GetAll(GetAllReceivablesRegistersInput input);

		Task<GetReceivablesRegisterForEditOutput> GetReceivablesRegisterForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditReceivablesRegisterDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}