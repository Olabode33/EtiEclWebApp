using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace TestDemo.LoanImpairmentApprovals.Dtos
{
    public class LoanImpairmentApprovalDto : EntityDto<Guid>
    {


    }

    public class LoanImpairmentAuditInfoDto
    {
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public List<LoanImpairmentApprovalAuditInfoDto> Approvals { get; set; }
    }
    public class LoanImpairmentApprovalAuditInfoDto
    {
        public DateTime ReviewedDate { get; set; }
        public CalibrationStatusEnum Status { get; set; }
        public string ReviewedBy { get; set; }
        public string ReviewComment { get; set; }
        public Guid RegistrationId { get; set; }
    }
}