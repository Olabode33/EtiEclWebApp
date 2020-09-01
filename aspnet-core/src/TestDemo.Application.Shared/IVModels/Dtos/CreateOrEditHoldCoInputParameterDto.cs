
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.IVModels.Dtos
{
    public class CreateOrEditHoldCoInputParameterDto : EntityDto<Guid?>
    {

		public DateTime ValuationDate { get; set; }
		
		
		public double Optimistic { get; set; }
		
		
		public double BestEstimate { get; set; }
		
		
		public double Downturn { get; set; }
		
		
		public string AssumedRating { get; set; }
		
		
		public string DefaultLoanRating { get; set; }
		
		
		public double RecoveryRate { get; set; }
		
		
		public DateTime AssumedStartDate { get; set; }
		
		
		public DateTime AssumedMaturityDate { get; set; }
		
		

    }
}