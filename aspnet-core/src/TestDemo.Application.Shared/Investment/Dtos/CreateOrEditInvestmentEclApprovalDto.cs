using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.Investment.Dtos
{
    public class CreateOrEditInvestmentEclApprovalDto : EntityDto<Guid?>
    {

		public DateTime ReviewedDate { get; set; }
		
		
		public string ReviewComment { get; set; }
		
		
		public GeneralStatusEnum Status { get; set; }
		
		
		 public long? ReviewedByUserId { get; set; }
		 
		 		 public Guid InvestmentEclId { get; set; }
		 
		 
    }
}