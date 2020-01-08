using Abp.Application.Services.Dto;
using System;

namespace TestDemo.Investment.Dtos
{
    public class GetAllInvestmentEclsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int StatusFilter { get; set; }


		 public string UserNameFilter { get; set; }

		 
    }
}