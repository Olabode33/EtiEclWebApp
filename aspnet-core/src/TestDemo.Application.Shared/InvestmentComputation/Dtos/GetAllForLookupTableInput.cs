﻿using Abp.Application.Services.Dto;

namespace TestDemo.InvestmentComputation.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}