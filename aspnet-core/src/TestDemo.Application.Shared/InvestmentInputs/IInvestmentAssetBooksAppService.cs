using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.InvestmentInputs.Dtos;
using TestDemo.Dto;

namespace TestDemo.InvestmentInputs
{
    public interface IInvestmentAssetBooksAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvestmentAssetBookForViewDto>> GetAll(GetAllInvestmentAssetBooksInput input);

		Task<GetInvestmentAssetBookForEditOutput> GetInvestmentAssetBookForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditInvestmentAssetBookDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<InvestmentAssetBookInvestmentEclUploadLookupTableDto>> GetAllInvestmentEclUploadForLookupTable(GetAllForLookupTableInput input);
		
    }
}