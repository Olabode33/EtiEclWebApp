using TestDemo.EclShared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.PdCalibrationResult
{
	[Table("CalibrationResultPdScenarioMacroeconomicProjections")]
    [Audited]
    public class CalibrationResultPdScenarioMacroeconomicProjection : FullAuditedEntity<Guid>
    {

		public virtual ModuleEnum Module { get; set; }
		
		public virtual DateTime Date { get; set; }
		
		public virtual double PrimeLendingRate { get; set; }
		
		public virtual double OilExport { get; set; }
		
		public virtual double RealGdpGrowthRate { get; set; }
		
		public virtual double DifferencedRealGdp { get; set; }
		

    }
}