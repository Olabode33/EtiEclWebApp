using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.RetailAssumption.Dtos
{
    public class RetailEclAssumptionApprovalsDto : EntityDto<Guid>
    {
		public AssumptionTypeEnum AssumptionType { get; set; }

		public string OldValue { get; set; }

		public string NewValue { get; set; }

		public DateTime? DateReviewed { get; set; }

		public string ReviewComment { get; set; }

		public GeneralStatusEnum Status { get; set; }

		public bool RequiresGroupApproval { get; set; }


		 public long? ReviewedByUserId { get; set; }

		 		 public Guid? RetailEclId { get; set; }

		 
    }
}