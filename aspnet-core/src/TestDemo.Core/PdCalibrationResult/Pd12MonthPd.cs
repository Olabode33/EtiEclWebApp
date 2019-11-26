using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.PdCalibrationResult
{
	[Table("CalibrationResult12MonthPds")]
    [Audited]
    public class CalibrationResult12MonthPd : FullAuditedEntity<Guid>
    {

		public virtual int Credit { get; set; }
		
		public virtual double? Pd { get; set; }
		
		public virtual string SnPMappingEtiCreditPolicy { get; set; }
		
		public virtual string SnPMappingBestFit { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }
		

    }
}