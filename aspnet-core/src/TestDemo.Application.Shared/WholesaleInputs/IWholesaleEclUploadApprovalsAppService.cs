using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleInputs.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleInputs
{
    public interface IWholesaleEclUploadApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclUploadApprovalForViewDto>> GetAll(GetAllWholesaleEclUploadApprovalsInput input);

		Task<GetWholesaleEclUploadApprovalForEditOutput> GetWholesaleEclUploadApprovalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclUploadApprovalDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclUploadApprovalWholesaleEclUploadLookupTableDto>> GetAllWholesaleEclUploadForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<WholesaleEclUploadApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
    }
}