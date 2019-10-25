using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.OBE;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.ObeInputs
{
	[Table("ObeEclUploads")]
    [Audited]
    public class ObeEclUpload : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual UploadDocTypeEnum DocType { get; set; }
		
		public virtual string UploadComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		

		public virtual Guid? ObeEclId { get; set; }
		
        [ForeignKey("ObeEclId")]
		public ObeEcl ObeEclFk { get; set; }
		
    }
}