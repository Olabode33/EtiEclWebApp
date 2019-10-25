
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.RetailInputs.Dtos
{
    public class RetailEclDataPaymentScheduleDto : EntityDto<Guid>
    {
		public string ContractRefNo { get; set; }

		public DateTime? StartDate { get; set; }

		public string Component { get; set; }

		public int? NoOfSchedules { get; set; }

		public string Frequency { get; set; }

		public double? Amount { get; set; }


		 public Guid RetailEclUploadId { get; set; }

		 
    }
}