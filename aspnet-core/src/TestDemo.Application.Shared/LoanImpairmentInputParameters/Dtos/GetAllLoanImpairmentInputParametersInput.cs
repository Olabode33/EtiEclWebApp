using Abp.Application.Services.Dto;
using System;

namespace TestDemo.LoanImpairmentInputParameters.Dtos
{
    public class GetAllLoanImpairmentInputParametersInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}