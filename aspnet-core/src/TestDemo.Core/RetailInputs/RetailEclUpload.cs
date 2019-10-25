using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.RetailInputs
{
	[Table("RetailEclUploads")]
    [Audited]
    public class RetailEclUpload : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual UploadDocTypeEnum DocType { get; set; }
		
		public virtual string UploadComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		

		public virtual Guid RetailEclId { get; set; }
		
        [ForeignKey("RetailEclId")]
		public RetailEcl RetailEclFk { get; set; }
		
    }
}