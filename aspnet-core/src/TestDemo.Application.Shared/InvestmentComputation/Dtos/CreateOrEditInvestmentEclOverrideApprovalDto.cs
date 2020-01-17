using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentComputation.Dtos
{
    public class CreateOrEditInvestmentEclOverrideApprovalDto : EntityDto<Guid?>
    {

		public DateTime? ReviewDate { get; set; }
		
		
		public string ReviewComment { get; set; }
		
		
		public GeneralStatusEnum Status { get; set; }
		
		
		 public long? ReviewedByUserId { get; set; }
		 
		 		 public Guid? InvestmentEclOverrideId { get; set; }
		 
		 
    }
}