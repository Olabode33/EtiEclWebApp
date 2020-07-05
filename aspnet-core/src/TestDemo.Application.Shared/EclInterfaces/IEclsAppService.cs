using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestDemo.Dto;
using TestDemo.Dto.Approvals;
using TestDemo.Dto.Ecls;
using TestDemo.EclShared.Dtos;

namespace TestDemo.EclInterfaces
{
    public interface IEclsAppService: IApplicationService 
    {
        Task<GetEclForEditOutput> GetEclDetailsForEdit(EntityDto<Guid> input);
        Task CreateOrEdit(CreateOrEditEclDto input);
        Task<Guid> CreateEclAndAssumption(CreateOrEditEclDto input);
        //Task<Guid> CreateAndGetId(long ouId);
        //Task SaveEadInputAssumption(long ouId, Guid eclId);
        //Task SaveLgdInputAssumption(long ouId, Guid eclId);
        //Task SavePdInputAssumption(long ouId, Guid eclId);
        //Task SavePdMacroAssumption(long ouId, Guid eclId);
        //Task SavePdFitchAssumption(long ouId, Guid eclId);
        Task Delete(EntityDto<Guid> input);
        Task SubmitForApproval(EntityDto<Guid> input);
        Task ApproveReject(CreateOrEditEclApprovalDto input);
        Task RunEcl(EntityDto<Guid> input);
        Task RunPostEcl(EntityDto<Guid> input);
        Task GenerateReport(EntityDto<Guid> input);
        Task<FileDto> DownloadReport(EntityDto<Guid> input);
        Task CloseEcl(EntityDto<Guid> input);
        Task ReopenEcl(EntityDto<Guid> input);
        //Task<ValidationMessageDto> ValidateForSubmission(Guid eclId);
        ///Task<ValidationMessageDto> ValidateForPostRun(Guid eclId);
    }
}
