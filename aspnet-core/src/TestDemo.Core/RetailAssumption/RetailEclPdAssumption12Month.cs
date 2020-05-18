using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;
using TestDemo.EclLibrary.BaseEngine.AssumptionBase;

namespace TestDemo.RetailAssumption
{
	[Table("RetailEclPdAssumption12Months")]
    [Audited]
    public class RetailEclPdAssumption12Month : EclPdAssumption12MonthBase
	{
		public virtual Guid? RetailEclId { get; set; }
		
        [ForeignKey("RetailEclId")]
		public RetailEcl RetailEclFk { get; set; }
		
    }
}