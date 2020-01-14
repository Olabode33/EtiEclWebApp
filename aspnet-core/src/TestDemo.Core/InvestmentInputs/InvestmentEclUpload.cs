using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.Investment;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.InvestmentInputs
{
	[Table("InvestmentEclUploads")]
    [Audited]
    public class InvestmentEclUpload : FullAuditedEntity<Guid> 
    {

		public virtual UploadDocTypeEnum DocType { get; set; }
		
		public virtual string UploadComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		

		public virtual Guid InvestmentEclId { get; set; }
		
        [ForeignKey("InvestmentEclId")]
		public InvestmentEcl InvestmentEclFk { get; set; }
		
    }
}