using Abp.Organizations;
using TestDemo.EclShared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.AffiliateMacroEconomicVariable
{
	[Table("AffiliateMacroEconomicVariableOffsets")]
    public class AffiliateMacroEconomicVariableOffset : Entity 
    {

		public virtual int BackwardOffset { get; set; }
		

		public virtual long AffiliateId { get; set; }
		
        [ForeignKey("AffiliateId")]
		public OrganizationUnit AffiliateFk { get; set; }
		
		public virtual int MacroeconomicVariableId { get; set; }
		
        [ForeignKey("MacroeconomicVariableId")]
		public MacroeconomicVariable MacroeconomicVariableFk { get; set; }
		
    }
}