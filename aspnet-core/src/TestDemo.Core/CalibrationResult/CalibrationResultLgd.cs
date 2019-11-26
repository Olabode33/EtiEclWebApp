using TestDemo.EclShared;
using TestDemo.EclShared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.LgdCalibrationResult
{
	[Table("CalibrationResultLgds")]
    [Audited]
    public class CalibrationResultLgd : FullAuditedEntity<Guid> 
    {

		public virtual CalibrationResultGroupEnum ResultGroup { get; set; }
		
		public virtual string InputName { get; set; }
		
		public virtual string InputValue { get; set; }
		
		public virtual DataTypeEnum DataType { get; set; }
		

    }
}