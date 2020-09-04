using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace TestDemo.ReceivablesApprovals.Dtos
{
    public class ReceivablesApprovalDto : EntityDto<Guid>
    {


    }

    public class ReceivablesAuditInfoDto
    {
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public List<ReceivablesApprovalAuditInfoDto> Approvals { get; set; }
    }
    public class ReceivablesApprovalAuditInfoDto
    {
        public DateTime ReviewedDate { get; set; }
        public CalibrationStatusEnum Status { get; set; }
        public string ReviewedBy { get; set; }
        public string ReviewComment { get; set; }
        public Guid RegisterId { get; set; }
    }
}