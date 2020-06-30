using Abp.Auditing;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.Authorization.Users;
using TestDemo.EclLibrary.BaseEngine.CalibrationRunBase;
using TestDemo.EclShared;

namespace TestDemo.Calibration
{
    [Table("CalibrationRunMacroAnalysis")]
    [Audited]
    public class MacroAnalysis: FullAuditedEntity
    {
        public virtual long OrganizationUnitId { get; set; }
        public virtual DateTime? ClosedDate { get; set; }
        public virtual CalibrationStatusEnum Status { get; set; }
        public virtual FrameworkEnum ModelType { get; set; }
        public virtual string ExceptionComment { get; set; }
        public virtual long? CloseByUserId { get; set; }
        [ForeignKey("CloseByUserId")]
        public User CloseByUserFk { get; set; }

    }
}
