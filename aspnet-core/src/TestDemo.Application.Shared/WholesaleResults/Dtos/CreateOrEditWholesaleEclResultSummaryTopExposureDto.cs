
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleResults.Dtos
{
    public class CreateOrEditWholesaleEclResultSummaryTopExposureDto : EntityDto<Guid?>
    {

		public double? PreOverrideExposure { get; set; }
		
		
		public double? PreOverrideImpairment { get; set; }
		
		
		public double? PreOverrideCoverageRatio { get; set; }
		
		
		public double? PostOverrideExposure { get; set; }
		
		
		public double? PostOverrideImpairment { get; set; }
		
		
		public double? PostOverrideCoverageRatio { get; set; }
		
		
		public string ContractId { get; set; }
		
		
		 public Guid WholesaleEclId { get; set; }
		 
		 		 public Guid? WholesaleEclDataLoanBookId { get; set; }
		 
		 
    }
}