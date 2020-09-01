using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.HoldCoAssetBook.Dtos;
using TestDemo.Dto;


namespace TestDemo.HoldCoAssetBook
{
    public interface IAssetBooksAppService : IApplicationService 
    {
        Task<PagedResultDto<GetAssetBookForViewDto>> GetAll(GetAllAssetBooksInput input);

		Task<GetAssetBookForEditOutput> GetAssetBookForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditAssetBookDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}