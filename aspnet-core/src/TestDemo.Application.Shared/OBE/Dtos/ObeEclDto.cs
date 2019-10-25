using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.OBE.Dtos
{
    public class ObeEclDto : EntityDto<Guid>
    {
		public DateTime ReportingDate { get; set; }

		public DateTime? ClosedDate { get; set; }

		public bool IsApproved { get; set; }

		public EclStatusEnum Status { get; set; }


		 public long? ClosedByUserId { get; set; }

		 
    }
}