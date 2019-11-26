using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.PdCalibrationResult
{
	[Table("CalibrationResultPdMarginalDefaultRates")]
    [Audited]
    public class CalibrationResultPdMarginalDefaultRate : FullAuditedEntity<Guid>
    {

		public virtual int Month { get; set; }
		
		public virtual string PdGroup { get; set; }
		
		public virtual double? Value { get; set; }
		

    }
}