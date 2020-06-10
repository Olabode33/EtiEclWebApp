using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.Calibration.Dtos;
using TestDemo.Dto;
using System.Collections.Generic;
using TestDemo.Dto.Approvals;

namespace TestDemo.Calibration
{
    public interface ICalibrationsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetCalibrationRunForViewDto>> GetAll(GetAllCalibrationRunInput input);
		Task<GetCalibrationRunForEditOutput> GetCalibrationForEdit(EntityDto<Guid> input);
		Task<Guid> CreateOrEdit(CreateOrEditCalibrationRunDto input);
		Task Delete(EntityDto<Guid> input);

		Task SubmitForApproval(EntityDto<Guid> input);
		Task ApproveReject(CreateOrEditEclApprovalDto input);
		Task RunCalibration(EntityDto<Guid> input);
		Task GenerateReport(EntityDto<Guid> input);
		Task ApplyToEcl(EntityDto<Guid> input);
		Task CloseCalibration(EntityDto<Guid> input);

		Task<List<CalibrationEadBehaviouralTermUserLookupTableDto>> GetAllUserForTableDropdown();
		
    }
}