using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.OBE;
using TestDemo.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;
using TestDemo.EclLibrary.BaseEngine.AssumptionBase;

namespace TestDemo.ObeAssumption
{
	[Table("ObeEclAssumptionApprovals")]
    [Audited]
    public class ObeEclAssumptionApproval : EclAssumptionApprovalBase
	{
		public virtual Guid? ObeEclId { get; set; }
		
        [ForeignKey("ObeEclId")]
		public ObeEcl ObeEclFk { get; set; }		
    }
}