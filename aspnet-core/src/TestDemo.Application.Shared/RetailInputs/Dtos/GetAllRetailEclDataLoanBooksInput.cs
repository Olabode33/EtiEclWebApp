using Abp.Application.Services.Dto;
using System;

namespace TestDemo.RetailInputs.Dtos
{
    public class GetAllRetailEclDataLoanBooksInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string CustomerNoFilter { get; set; }

		public string AccountNoFilter { get; set; }

		public string ContractNoFilter { get; set; }

		public string CustomerNameFilter { get; set; }		 
    }
}