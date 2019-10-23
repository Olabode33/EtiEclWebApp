
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class CreateOrEditWholesaleEclPdAssumption12MonthsDto : EntityDto<Guid?>
    {

		[Required]
		public int Credit { get; set; }
		
		
		public double? PD { get; set; }
		
		
		public string SnPMappingEtiCreditPolicy { get; set; }
		
		
		public string SnPMappingBestFit { get; set; }
		
		
		 public Guid? WholesaleEclId { get; set; }
		 
		 
    }
}