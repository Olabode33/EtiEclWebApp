using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.EclShared
{
	[Table("MacroeconomicVariables")]
    [Audited]
    public class MacroeconomicVariable : FullAuditedEntity 
    {

		public virtual string Name { get; set; }
		
		public virtual string Description { get; set; }
		

    }
}