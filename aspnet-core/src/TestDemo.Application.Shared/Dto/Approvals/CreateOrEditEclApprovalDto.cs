using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto.Approvals
{
    public class CreateOrEditEclApprovalDto
    {
		public DateTime ReviewedDate { get; set; }
		public string ReviewComment { get; set; }
		public GeneralStatusEnum Status { get; set; }
		public long? ReviewedByUserId { get; set; }
		public Guid EclId { get; set; }
	}
}
