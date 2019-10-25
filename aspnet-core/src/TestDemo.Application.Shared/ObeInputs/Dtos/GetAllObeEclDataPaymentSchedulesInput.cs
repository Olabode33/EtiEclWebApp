using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ObeInputs.Dtos
{
    public class GetAllObeEclDataPaymentSchedulesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string ContractRefNoFilter { get; set; }

		public DateTime? MaxStartDateFilter { get; set; }
		public DateTime? MinStartDateFilter { get; set; }

		public string ComponentFilter { get; set; }

		public int? MaxNoOfSchedulesFilter { get; set; }
		public int? MinNoOfSchedulesFilter { get; set; }

		public string FrequencyFilter { get; set; }

		public double? MaxAmountFilter { get; set; }
		public double? MinAmountFilter { get; set; }


		 public string ObeEclUploadTenantIdFilter { get; set; }

		 
    }
}