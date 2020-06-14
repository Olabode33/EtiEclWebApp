using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.Authorization.Users;
using TestDemo.EclLibrary.BaseEngine.CalibrationRunBase;
using TestDemo.EclShared;

namespace TestDemo.Calibration.Approvals
{
    [Table("MacroAnalysisApprovals")]
    public class MacroAnalysisApproval: FullAuditedEntity
    {
        public virtual long OrganizationUnitId { get; set; }
        public virtual DateTime ReviewedDate { get; set; }
        public virtual string ReviewComment { get; set; }
        public virtual GeneralStatusEnum Status { get; set; }
        public virtual long? ReviewedByUserId { get; set; }
        [ForeignKey("ReviewedByUserId")]
        public User ReviewedByUserFk { get; set; }
        public int? MacroId { get; set; }
        [ForeignKey("MacroId")]
        public MacroAnalysis CalibrationFk { get; set; }
    }
}
