using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class CreateOrEditWholesaleEclAssumptionApprovalDto : EntityDto<Guid?>
    {

		public AssumptionTypeEnum AssumptionType { get; set; }
		
		
		public Guid AssumptionId { get; set; }
		
		
		public string OldValue { get; set; }
		
		
		public string NewValue { get; set; }
		
		
		public DateTime? DateReviewed { get; set; }
		
		
		public string ReviewComment { get; set; }
		
		
		public GeneralStatusEnum Status { get; set; }
		
		
		 public Guid WholesaleEclId { get; set; }
		 
		 		 public long? ReviewedByUserId { get; set; }
		 
		 
    }
}