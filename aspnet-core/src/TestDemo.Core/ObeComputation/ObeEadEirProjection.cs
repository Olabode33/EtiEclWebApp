using TestDemo.OBE;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;

namespace TestDemo.ObeComputation
{
	[Table("ObeEadEirProjections")]
    public class ObeEadEirProjection : EclEadEirProjectionBase
	{
		public virtual Guid ObeEclId { get; set; }
		
    }
}