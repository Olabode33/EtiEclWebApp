﻿using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetAllWholesaleEclPdAssumptionMacroeconomicProjectionsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string WholesaleEclTenantIdFilter { get; set; }

		 
    }
}