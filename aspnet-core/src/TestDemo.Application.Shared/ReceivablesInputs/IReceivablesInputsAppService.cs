using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ReceivablesInputs.Dtos;
using TestDemo.Dto;


namespace TestDemo.ReceivablesInputs
{
    public interface IReceivablesInputsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetReceivablesInputForViewDto>> GetAll(GetAllReceivablesInputsInput input);

        Task<GetReceivablesInputForViewDto> GetReceivablesInputForView(Guid id);

		Task<GetReceivablesInputForEditOutput> GetReceivablesInputForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditReceivablesInputDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}