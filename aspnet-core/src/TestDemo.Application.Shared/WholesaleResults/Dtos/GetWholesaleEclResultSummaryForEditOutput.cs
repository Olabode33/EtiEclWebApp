using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleResults.Dtos
{
    public class GetWholesaleEclResultSummaryForEditOutput
    {
		public CreateOrEditWholesaleEclResultSummaryDto WholesaleEclResultSummary { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}