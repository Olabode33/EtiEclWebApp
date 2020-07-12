using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;
using TestDemo.EclLibrary.BaseEngine.InputBase;

namespace TestDemo.BatchEcls.BatchEclInput
{
	[Table("BatchEclUploads")]
    [Audited]
    public class BatchEclUpload : EclUploadBase
	{
		public virtual int CountWholesaleData { get; set; }
		public virtual int CountRetailData { get; set; }
		public virtual int CountObeData { get; set; }
		public virtual int CountTotalData { get; set; }
		public virtual Guid BatchId { get; set; }
        [ForeignKey("BatchEclId")]
		public BatchEcl BatchEclFk { get; set; }
		
    }
}