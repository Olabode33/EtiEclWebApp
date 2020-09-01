using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.HoldCoResult
{
	[Table("ResultSummaryByStages")]
    public class ResultSummaryByStage : FullAuditedEntity<Guid> 
    {

		public virtual double StageOneExposure { get; set; }
		
		public virtual double StageTwoExposure { get; set; }
		
		public virtual double StageThreeExposure { get; set; }
		
		public virtual double TotalExposure { get; set; }
		
		public virtual double StageOneImpairment { get; set; }
		
		public virtual double StageTwoImpairment { get; set; }
		
		public virtual double StageThreeImpairment { get; set; }
		
		public virtual double StageOneImpairmentRatio { get; set; }
		
		public virtual double StageTwoImpairmentRatio { get; set; }
		
		public virtual double TotalImpairment { get; set; }
		
		public virtual double StageThreeImpairmentRatio { get; set; }
		
		public virtual double TotalImpairmentRatio { get; set; }
		
		public virtual Guid RegistrationId { get; set; }
		

    }
}