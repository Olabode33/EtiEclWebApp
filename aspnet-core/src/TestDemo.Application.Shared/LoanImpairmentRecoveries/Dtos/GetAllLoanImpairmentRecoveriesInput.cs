using Abp.Application.Services.Dto;
using System;

namespace TestDemo.LoanImpairmentRecoveries.Dtos
{
    public class GetAllLoanImpairmentRecoveriesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}