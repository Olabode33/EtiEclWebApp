using Abp.Application.Services.Dto;
using System;

namespace TestDemo.LoanImpairmentScenarios.Dtos
{
    public class GetAllLoanImpairmentScenariosInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}