using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace TestDemo.HoldCoApprovals.Dtos
{
    public class HoldCoApprovalDto : EntityDto
    {


    }

    public class HoldCoAuditInfoDto
    {
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public List<HoldCoApprovalAuditInfoDto> Approvals { get; set; }
    }
    public class HoldCoApprovalAuditInfoDto
    {
        public DateTime ReviewedDate { get; set; }
        public CalibrationStatusEnum Status { get; set; }
        public string ReviewedBy { get; set; }
        public string ReviewComment { get; set; }
        public Guid RegistrationId { get; set; }
    }
}