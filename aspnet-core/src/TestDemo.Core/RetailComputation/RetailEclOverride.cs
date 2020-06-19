using TestDemo.RetailInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;

namespace TestDemo.RetailComputation
{
	[Table("RetailEclOverrides")]
    public class RetailEclOverride : EclOverrideBase
	{
		public virtual Guid? RetailEclId { get; set; }
		public virtual Guid? RetailEclDataLoanBookId { get; set; }
    }
}