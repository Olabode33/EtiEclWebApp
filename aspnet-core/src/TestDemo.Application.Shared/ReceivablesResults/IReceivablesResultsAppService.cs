using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ReceivablesResults.Dtos;
using TestDemo.Dto;


namespace TestDemo.ReceivablesResults
{
    public interface IReceivablesResultsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetReceivablesResultForViewDto>> GetAll(GetAllReceivablesResultsInput input);

        Task<GetReceivablesResultForViewDto> GetReceivablesResultForView(Guid id);

		Task<GetReceivablesResultForEditOutput> GetReceivablesResultForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditReceivablesResultDto input);

		Task Delete(EntityDto<Guid> input);

		Task<FileDto> GetReceivablesResultsToExcel(GetAllReceivablesResultsForExcelInput input);

		
    }
}