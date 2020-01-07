using TestDemo.EclShared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.EclConfig
{
	[Table("EclConfigurations")]
    [Audited]
    public class EclConfiguration : Entity 
    {

		public virtual string PropertyKey { get; set; }
		
		public virtual string DisplayName { get; set; }
		
		public virtual string Value { get; set; }
		
		public virtual DataTypeEnum DataType { get; set; }
		

    }
}