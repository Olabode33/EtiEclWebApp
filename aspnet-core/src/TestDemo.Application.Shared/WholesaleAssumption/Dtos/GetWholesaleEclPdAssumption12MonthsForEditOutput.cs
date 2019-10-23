using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetWholesaleEclPdAssumption12MonthsForEditOutput
    {
		public CreateOrEditWholesaleEclPdAssumption12MonthsDto WholesaleEclPdAssumption12Months { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}