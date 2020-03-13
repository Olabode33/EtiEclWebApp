using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto
{
    public class EclAuditInfoDto
    {
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public List<EclApprovalAuditInfoDto> Approvals { get; set; }
    }
    public class EclApprovalAuditInfoDto
    {
        public DateTime ReviewedDate { get; set; }
        public GeneralStatusEnum Status { get; set; }
        public string ReviewedBy { get; set; }
        public string ReviewComment { get; set; }
        public Guid EclId { get; set; }
    }
}
