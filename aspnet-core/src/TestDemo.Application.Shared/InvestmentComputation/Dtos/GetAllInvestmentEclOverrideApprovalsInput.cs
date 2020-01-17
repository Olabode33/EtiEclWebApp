using Abp.Application.Services.Dto;
using System;

namespace TestDemo.InvestmentComputation.Dtos
{
    public class GetAllInvestmentEclOverrideApprovalsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int StatusFilter { get; set; }


		 public string UserNameFilter { get; set; }

		 		 public string InvestmentEclOverrideOverrideCommentFilter { get; set; }

		 
    }
}