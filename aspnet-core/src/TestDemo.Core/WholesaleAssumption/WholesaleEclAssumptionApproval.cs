using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.Wholesale;
using TestDemo.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;
using TestDemo.EclLibrary.BaseEngine.AssumptionBase;

namespace TestDemo.WholesaleAssumption
{
	[Table("WholesaleEclAssumptionApprovals")]
    [Audited]
    public class WholesaleEclAssumptionApproval : EclAssumptionApprovalBase
	{
		public virtual Guid WholesaleEclId { get; set; }
		
        [ForeignKey("WholesaleEclId")]
		public WholesaleEcl WholesaleEclFk { get; set; }
		
		
    }
}