using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeResults.Dtos
{
    public class GetObesaleEclResultSummaryForEditOutput
    {
		public CreateOrEditObesaleEclResultSummaryDto ObesaleEclResultSummary { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}