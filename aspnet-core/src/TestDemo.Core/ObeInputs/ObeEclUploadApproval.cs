using TestDemo.EclShared;
using TestDemo.ObeInputs;
using TestDemo.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;
using TestDemo.EclLibrary.BaseEngine.InputBase;

namespace TestDemo.ObeInputs
{
	[Table("ObeEclUploadApprovals")]
    [Audited]
    public class ObeEclUploadApproval : EclUploadApprovalBase
    {
		public virtual Guid? ObeEclUploadId { get; set; }
		
        [ForeignKey("ObeEclUploadId")]
		public ObeEclUpload ObeEclUploadFk { get; set; }
		
    }
}