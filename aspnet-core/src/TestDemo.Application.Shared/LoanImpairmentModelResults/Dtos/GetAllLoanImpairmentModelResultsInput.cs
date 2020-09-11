using Abp.Application.Services.Dto;
using System;

namespace TestDemo.LoanImpairmentModelResults.Dtos
{
    public class GetAllLoanImpairmentModelResultsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}