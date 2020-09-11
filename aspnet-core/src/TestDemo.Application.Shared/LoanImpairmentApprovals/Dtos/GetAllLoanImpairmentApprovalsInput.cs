using Abp.Application.Services.Dto;
using System;

namespace TestDemo.LoanImpairmentApprovals.Dtos
{
    public class GetAllLoanImpairmentApprovalsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}