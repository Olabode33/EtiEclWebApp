using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.Investment.Dtos
{
    public class InvestmentEclApprovalDto : EntityDto<Guid>
    {
		public DateTime ReviewedDate { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public long? ReviewedByUserId { get; set; }

		 		 public Guid InvestmentEclId { get; set; }

		 
    }
}