using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleInputs.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleInputs
{
    public interface IWholesaleEclUploadsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclUploadForViewDto>> GetAll(GetAllWholesaleEclUploadsInput input);

		Task<GetWholesaleEclUploadForEditOutput> GetWholesaleEclUploadForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclUploadDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclUploadWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}