using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.ReceivablesCurrentPeriodDates
{
	[Table("CurrentPeriodDates")]
    public class CurrentPeriodDate : Entity<Guid> 
    {

		public virtual string Account { get; set; }
		
		public virtual double ZeroTo90 { get; set; }
		
		public virtual double NinetyOneTo180 { get; set; }
		
		public virtual double OneEightyOneTo365 { get; set; }
		
		public virtual double Over365 { get; set; }
		
		public virtual Guid RegisterId { get; set; }
		

    }
}