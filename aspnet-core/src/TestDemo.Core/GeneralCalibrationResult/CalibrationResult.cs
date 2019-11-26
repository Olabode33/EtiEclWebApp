using TestDemo.EclShared;
using TestDemo.EclShared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.GeneralCalibrationResult
{
	[Table("CalibrationResults")]
    [Audited]
    public class GeneralCalibrationResult : FullAuditedEntity 
    {
		public virtual CalibrationResultGroupEnum Group { get; set; }
		
		public virtual string Key { get; set; }
		
		public virtual string InputName { get; set; }
		
		public virtual string Value { get; set; }
		
		public virtual DataTypeEnum Type { get; set; }
		

    }
}