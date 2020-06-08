using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailInputs.Dtos;
using TestDemo.Dto;
using System.Collections.Generic;
using TestDemo.Dto.Inputs;

namespace TestDemo.RetailInputs
{
    public interface IRetailEclUploadsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclUploadForViewDto>> GetAll(GetAllRetailEclUploadsInput input);

        Task<List<GetEclUploadForViewDto>> GetEclUploads(EntityDto<Guid> input);


        Task<GetRetailEclUploadForEditOutput> GetRetailEclUploadForEdit(EntityDto<Guid> input);

        Task<Guid> CreateOrEdit(CreateOrEditRetailEclUploadDto input);


        Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclUploadRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}