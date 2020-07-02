using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.EclConfig
{
	[Table("OverrideTypes")]
    public class OverrideType : Entity 
    {

		public virtual string Name { get; set; }
		
		public virtual string Description { get; set; }
		

    }
}