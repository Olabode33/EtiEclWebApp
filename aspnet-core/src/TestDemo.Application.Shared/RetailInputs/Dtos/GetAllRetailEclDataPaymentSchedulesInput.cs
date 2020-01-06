using Abp.Application.Services.Dto;
using System;

namespace TestDemo.RetailInputs.Dtos
{
    public class GetAllRetailEclDataPaymentSchedulesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string ContractRefNoFilter { get; set; }
		 
    }
}