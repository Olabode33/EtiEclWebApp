using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.Authorization.Users;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.BaseEngine.CalibrationRunBase
{
    public class CalibrationRunBase : FullAuditedEntity<Guid>
    {
        public virtual long OrganizationUnitId { get; set; }
        public virtual DateTime? ClosedDate { get; set; }
        public virtual CalibrationStatusEnum Status { get; set; }
        public virtual FrameworkEnum ModelType { get; set; }
        public virtual string ExceptionComment { get; set; }
        public virtual string FriendlyException { get; set; }
        public virtual int ServiceId { get; set; }
        public virtual long? CloseByUserId { get; set; }
        [ForeignKey("CloseByUserId")]
        public User CloseByUserFk { get; set; }
    }
}
