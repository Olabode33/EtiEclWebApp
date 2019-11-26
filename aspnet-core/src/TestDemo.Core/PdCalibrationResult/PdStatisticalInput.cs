using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.PdCalibrationResult
{
	[Table("CalibrationResultPdStatisticalInputs")]
    [Audited]
    public class CalibrationResultPdStatisticalInput : FullAuditedEntity<Guid>
    {

		public virtual string StatisticalInputs { get; set; }
		
		public virtual double PrimeLendingRate { get; set; }
		
		public virtual double OilExport { get; set; }
		
		public virtual double RealGdpGrowthRate { get; set; }
		

    }
}