
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class WholesaleEclPdAssumption12MonthsDto : EntityDto<Guid>
    {
		public int Credit { get; set; }

		public double? PD { get; set; }

		public string SnPMappingEtiCreditPolicy { get; set; }

		public string SnPMappingBestFit { get; set; }


		 public Guid? WholesaleEclId { get; set; }

		 
    }
}