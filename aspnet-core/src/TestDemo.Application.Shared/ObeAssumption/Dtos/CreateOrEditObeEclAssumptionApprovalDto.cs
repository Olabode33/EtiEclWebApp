using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class CreateOrEditObeEclAssumptionApprovalDto : EntityDto<Guid?>
    {

		public AssumptionTypeEnum AssumptionType { get; set; }
		
		
		public string OldValue { get; set; }
		
		
		public string NewValue { get; set; }
		
		
		public DateTime? DateReviewed { get; set; }
		
		
		public string ReviewComment { get; set; }
		
		
		public GeneralStatusEnum Status { get; set; }
		
		
		[Required]
		public bool RequiresGroupApproval { get; set; }
		
		
		 public Guid? ObeEclId { get; set; }
		 
		 		 public long? ReviewedByUserId { get; set; }
		 
		 
    }
}