using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.PdCalibrationResult
{
	[Table("CalibrationResultPdHistoricIndexes")]
    [Audited]
    public class CalibrationResultPdHistoricIndex : FullAuditedEntity<Guid>
    {

		public virtual DateTime Date { get; set; }
		
		public virtual double Actual { get; set; }
		
		public virtual double Standardised { get; set; }
		

    }
}