using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class WholesaleEclAssumptionApprovalDto : EntityDto<Guid>
    {
		public AssumptionTypeEnum AssumptionType { get; set; }

		public string OldValue { get; set; }

		public DateTime? DateReviewed { get; set; }


		 public Guid WholesaleEclId { get; set; }

		 		 public long? ReviewedByUserId { get; set; }

		 
    }
}