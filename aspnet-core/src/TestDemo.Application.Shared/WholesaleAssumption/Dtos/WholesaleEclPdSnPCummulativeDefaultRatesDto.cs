
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class WholesaleEclPdSnPCummulativeDefaultRatesDto : EntityDto<Guid>
    {
		public string Key { get; set; }

		public string Rating { get; set; }

		public int? Years { get; set; }

		public double? Value { get; set; }


		 public Guid? WholesaleEclId { get; set; }

		 
    }
}