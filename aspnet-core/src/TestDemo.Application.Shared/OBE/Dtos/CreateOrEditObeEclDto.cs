using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.OBE.Dtos
{
    public class CreateOrEditObeEclDto : EntityDto<Guid?>
    {

		[Required]
		public DateTime ReportingDate { get; set; }
		
		
		public DateTime? ClosedDate { get; set; }
		
		
		[Required]
		public bool IsApproved { get; set; }
		
		
		public EclStatusEnum Status { get; set; }
		
		
		 public long? ClosedByUserId { get; set; }
		 
		 
    }
}