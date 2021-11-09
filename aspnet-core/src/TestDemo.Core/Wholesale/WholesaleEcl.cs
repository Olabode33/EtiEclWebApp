using TestDemo.EclShared;
using TestDemo.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;

namespace TestDemo.Wholesale
{
    [Table("WholesaleEcls")]
    [Audited]
    public class WholesaleEcl : FullAuditedEntity<Guid>, IMayHaveTenant, IMustHaveOrganizationUnit
    {
        public int? TenantId { get; set; }
        public virtual long OrganizationUnitId { get; set; }

        [Required]
        public virtual DateTime ReportingDate { get; set; }

        public virtual DateTime? ClosedDate { get; set; }

        [Required]
        public virtual bool IsApproved { get; set; }

        public virtual EclStatusEnum Status { get; set; }

        public Guid? CalibrationEadBehaviouralTermId { get; set; }
        public Guid? CalibrationEadCcfSummaryId { get; set; }
        public Guid? CalibrationLgdHairCutId { get; set; }
        public Guid? CalibrationLgdRecoveryRateId { get; set; }
        public Guid? CalibrationPdCrDrId { get; set; }
        public Guid? CalibrationPdCommConsId { get; set; }
        public bool? DataExportedForCalibration { get; set; }

        public virtual string ExceptionComment { get; set; }
        public virtual string FriendlyException { get; set; }

        public virtual Guid? BatchId { get; set; }
        public virtual bool IsSingleBatch { get; set; }
        public virtual int ServiceId { get; set; }

        public virtual long? ClosedByUserId { get; set; }

        [ForeignKey("ClosedByUserId")]
        public User ClosedByUserFk { get; set; }

    }
}