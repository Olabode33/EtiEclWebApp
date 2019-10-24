
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleInputs.Dtos
{
    public class CreateOrEditWholesaleEclDataPaymentScheduleDto : EntityDto<Guid?>
    {

		public string ContractRefNo { get; set; }
		
		
		public DateTime? StartDate { get; set; }
		
		
		public string Component { get; set; }
		
		
		public int? NoOfSchedules { get; set; }
		
		
		public string Frequency { get; set; }
		
		
		public double? Amount { get; set; }
		
		
		 public Guid WholesaleEclUploadId { get; set; }
		 
		 
    }
}