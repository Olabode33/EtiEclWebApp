using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;

namespace TestDemo.RetailComputation
{
	[Table("RetailPdRedefaultLifetimeOptimistics")]
    public class RetailPdRedefaultLifetimeOptimistic : EclPdRedefaultLifetimeBase
	{
		public virtual Guid RetailEclId { get; set; }
		
    }
}