using Abp.Application.Services.Dto;
using System;

namespace TestDemo.LoanImpairmentsRegisters.Dtos
{
    public class GetAllLoanImpairmentRegistersInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? StatusFilter { get; set; }



    }
}