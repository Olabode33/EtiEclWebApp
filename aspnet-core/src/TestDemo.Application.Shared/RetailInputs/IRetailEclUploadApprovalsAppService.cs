using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailInputs.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailInputs
{
    public interface IRetailEclUploadApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclUploadApprovalForViewDto>> GetAll(GetAllRetailEclUploadApprovalsInput input);

		Task<GetRetailEclUploadApprovalForEditOutput> GetRetailEclUploadApprovalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclUploadApprovalDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclUploadApprovalRetailEclUploadLookupTableDto>> GetAllRetailEclUploadForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<RetailEclUploadApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
    }
}