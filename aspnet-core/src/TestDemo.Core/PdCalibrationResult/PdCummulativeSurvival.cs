using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.PdCalibrationResult
{
	[Table("CalibrationResultPdCummulativeSurvivals")]
    [Audited]
    public class CalibrationResultPdCummulativeSurvival : FullAuditedEntity<Guid>
    {

		public virtual int Month { get; set; }
		
		public virtual string PdGroup { get; set; }
		
		public virtual double Value { get; set; }
		

    }
}