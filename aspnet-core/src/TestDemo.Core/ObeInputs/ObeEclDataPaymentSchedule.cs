using TestDemo.ObeInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.ObeInputs
{
	[Table("ObeEclDataPaymentSchedules")]
    public class ObeEclDataPaymentSchedule : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string ContractRefNo { get; set; }
		
		public virtual DateTime? StartDate { get; set; }
		
		public virtual string Component { get; set; }
		
		public virtual int? NoOfSchedules { get; set; }
		
		public virtual string Frequency { get; set; }
		
		public virtual double? Amount { get; set; }
		

		public virtual Guid? ObeEclUploadId { get; set; }
		
        [ForeignKey("ObeEclUploadId")]
		public ObeEclUpload ObeEclUploadFk { get; set; }
		
    }
}