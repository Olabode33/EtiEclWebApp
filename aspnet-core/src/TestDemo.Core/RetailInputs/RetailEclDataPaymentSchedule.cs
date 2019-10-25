using TestDemo.RetailInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;

namespace TestDemo.RetailInputs
{
	[Table("RetailEclDataPaymentSchedules")]
    public class RetailEclDataPaymentSchedule : FullAuditedEntity<Guid> , IMayHaveTenant, IMustHaveOrganizationUnit
    {
			public int? TenantId { get; set; }
        public virtual long OrganizationUnitId { get; set; }


        public virtual string ContractRefNo { get; set; }
		
		public virtual DateTime? StartDate { get; set; }
		
		public virtual string Component { get; set; }
		
		public virtual int? NoOfSchedules { get; set; }
		
		public virtual string Frequency { get; set; }
		
		public virtual double? Amount { get; set; }
		

		public virtual Guid RetailEclUploadId { get; set; }
		
        [ForeignKey("RetailEclUploadId")]
		public RetailEclUpload RetailEclUploadFk { get; set; }
		
    }
}