using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;

namespace TestDemo.RetailComputation
{
	[Table("RetailPdRedefaultLifetimeBests")]
    public class RetailPdRedefaultLifetimeBest : EclPdRedefaultLifetimeBase
	{
		public virtual Guid RetailEclId { get; set; }
    }
}