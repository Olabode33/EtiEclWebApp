using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto.Ecls
{
    public class CreateOrEditEclDto: EntityDto<Guid>
    {
		public DateTime ReportingDate { get; set; }
		public DateTime? ClosedDate { get; set; }
		public bool IsApproved { get; set; }
		public EclStatusEnum Status { get; set; }
		public long? ClosedByUserId { get; set; }
	}
}
