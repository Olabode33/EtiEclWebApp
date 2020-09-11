using Abp.Application.Services.Dto;
using System;

namespace TestDemo.LoanImpairmentKeyParameters.Dtos
{
    public class GetAllLoanImpairmentKeyParametersInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}