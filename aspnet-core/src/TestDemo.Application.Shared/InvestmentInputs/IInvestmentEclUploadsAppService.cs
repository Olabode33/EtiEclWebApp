using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.InvestmentInputs.Dtos;
using TestDemo.Dto;

namespace TestDemo.InvestmentInputs
{
    public interface IInvestmentEclUploadsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvestmentEclUploadForViewDto>> GetAll(GetAllInvestmentEclUploadsInput input);

		Task<GetInvestmentEclUploadForEditOutput> GetInvestmentEclUploadForEdit(EntityDto<Guid> input);

        Task<Guid> CreateOrEdit(CreateOrEditInvestmentEclUploadDto input);


        Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<InvestmentEclUploadInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}