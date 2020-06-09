using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Dtos
{
    public class ReviewEclOverrideInputDto
    {
		public string ReviewComment { get; set; }
		public GeneralStatusEnum Status { get; set; }
		public Guid OverrideRecordId { get; set; }
		public Guid EclId { get; set; }
	}
}
