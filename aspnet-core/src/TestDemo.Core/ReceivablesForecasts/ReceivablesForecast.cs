using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.ReceivablesForecasts
{
	[Table("ReceivablesForecasts")]
    public class ReceivablesForecast : Entity<Guid> 
    {

		public virtual string Period { get; set; }
		
		public virtual double Optimistic { get; set; }
		
		public virtual double Base { get; set; }
		
		public virtual double Downturn { get; set; }
		
		public virtual Guid RegisterId { get; set; }
		

    }
}