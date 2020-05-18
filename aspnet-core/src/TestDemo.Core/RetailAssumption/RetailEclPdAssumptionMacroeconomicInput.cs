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
using TestDemo.EclLibrary.BaseEngine.AssumptionBase;

namespace TestDemo.RetailAssumption
{
	[Table("RetailEclPdAssumptionMacroeconomicInputs")]
    [Audited]
    public class RetailEclPdAssumptionMacroeconomicInput : EclPdAssumptionMacroeconomicInputBase
	{
		public virtual Guid RetailEclId { get; set; }
		
        [ForeignKey("RetailEclId")]
		public RetailEcl RetailEclFk { get; set; }
    }
}