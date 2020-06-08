using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Dto.Assumptions
{
    public class CreateOrEditEclPdSnPCummulativeDefaultRateDtoBase: EntityDto<Guid?>
    {
		public string Key { get; set; }
		public string Rating { get; set; }
		public int? Years { get; set; }
		public double? Value { get; set; }
		public bool RequiresGroupApproval { get; set; }
	}
}
