using TestDemo.EclShared;
using TestDemo.WholesaleInputs;
using TestDemo.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;
using TestDemo.EclLibrary.BaseEngine.InputBase;

namespace TestDemo.WholesaleInputs
{
	[Table("WholesaleEclUploadApprovals")]
    [Audited]
    public class WholesaleEclUploadApproval : EclUploadApprovalBase
    {
		public virtual Guid WholesaleEclUploadId { get; set; }
		
        [ForeignKey("WholesaleEclUploadId")]
		public WholesaleEclUpload WholesaleEclUploadFk { get; set; }
		
    }
}