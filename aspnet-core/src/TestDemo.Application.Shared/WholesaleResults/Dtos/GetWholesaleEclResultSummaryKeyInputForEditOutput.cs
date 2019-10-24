using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleResults.Dtos
{
    public class GetWholesaleEclResultSummaryKeyInputForEditOutput
    {
		public CreateOrEditWholesaleEclResultSummaryKeyInputDto WholesaleEclResultSummaryKeyInput { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}