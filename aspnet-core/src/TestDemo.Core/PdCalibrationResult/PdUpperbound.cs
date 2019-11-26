using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.PdCalibrationResult
{
	[Table("CalibrationResultPdUpperbounds")]
    [Audited]
    public class CalibrationResultPdUpperbound : FullAuditedEntity<Guid>
    {

		public virtual string Rating { get; set; }
		
		public virtual double? Upperbound12MonthPd { get; set; }
		

    }
}