
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.RetailAssumption.Dtos
{
    public class RetailEclPdAssumption12MonthDto : EntityDto<Guid>
    {
		public int Credit { get; set; }

		public double? PD { get; set; }

		public string SnPMappingEtiCreditPolicy { get; set; }

		public string SnPMappingBestFit { get; set; }

		public bool RequiresGroupApproval { get; set; }


		 public Guid? RetailEclId { get; set; }

		 
    }
}