﻿using Abp.Application.Services.Dto;

namespace TestDemo.LoanImpairmentApprovals.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}