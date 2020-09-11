using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.LoanImpairmentApprovals.Dtos
{
    public class CreateOrEditLoanImpairmentApprovalDto : EntityDto<Guid?>
    {

		public Guid RegisterId { get; set; }
		
		
		public string ReviewComment { get; set; }
		
		
		public long ReviewedByUserId { get; set; }
		
		
		public DateTime ReviewedDate { get; set; }
		
		
		public CalibrationStatusEnum Status { get; set; }
		
		

    }
}