using TestDemo.WholesaleInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;

namespace TestDemo.WholesaleComputation
{
	[Table("WholesaleEclOverrides")]
    public class WholesaleEclOverride : EclOverrideBase
	{
		public virtual Guid? WholesaleEclId { get; set; }
		public virtual Guid? WholesaleEclDataLoanBookId { get; set; }
    }
}