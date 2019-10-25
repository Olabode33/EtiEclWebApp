using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.Retail.Dtos
{
    public class RetailEclDto : EntityDto<Guid>
    {
		public DateTime ReportingDate { get; set; }

		public DateTime? ClosedDate { get; set; }

		public bool IsApproved { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public long? ClosedByUserId { get; set; }

		 
    }
}