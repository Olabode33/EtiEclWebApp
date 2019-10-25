using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailInputs.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailInputs
{
    public interface IRetailEclUploadsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclUploadForViewDto>> GetAll(GetAllRetailEclUploadsInput input);

		Task<GetRetailEclUploadForEditOutput> GetRetailEclUploadForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclUploadDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclUploadRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}