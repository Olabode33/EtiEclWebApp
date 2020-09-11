using Abp.Application.Services.Dto;
using System;

namespace TestDemo.LoanImpairmentHaircuts.Dtos
{
    public class GetAllLoanImpairmentHaircutsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}