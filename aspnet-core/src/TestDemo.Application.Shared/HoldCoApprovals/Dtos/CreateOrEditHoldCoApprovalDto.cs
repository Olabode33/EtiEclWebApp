using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.HoldCoApprovals.Dtos
{
    public class CreateOrEditHoldCoApprovalDto : EntityDto<int?>
    {

		public Guid RegistrationId { get; set; }
		
		
		public string ReviewComment { get; set; }
		
		
		public long ReviewedByUserId { get; set; }
		
		
		public DateTime ReviewedDate { get; set; }
		
		
		public CalibrationStatusEnum Status { get; set; }
		
		

    }
}