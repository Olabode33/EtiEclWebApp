using TestDemo.EclShared;
using TestDemo.RetailInputs;
using TestDemo.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;
using TestDemo.EclLibrary.BaseEngine.InputBase;

namespace TestDemo.RetailInputs
{
	[Table("RetailEclUploadApprovals")]
    [Audited]
    public class RetailEclUploadApproval : EclUploadApprovalBase
	{
		public virtual Guid RetailEclUploadId { get; set; }
		
        [ForeignKey("RetailEclUploadId")]
		public RetailEclUpload RetailEclUploadFk { get; set; }
		
    }
}