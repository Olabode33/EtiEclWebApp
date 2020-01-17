using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.InvestmentComputation.Dtos
{
    public class InvestmentEclOverrideApprovalDto : EntityDto<Guid>
    {
		public DateTime? ReviewDate { get; set; }

		public string ReviewComment { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public long? ReviewedByUserId { get; set; }

		 		 public Guid? InvestmentEclOverrideId { get; set; }

		 
    }
}