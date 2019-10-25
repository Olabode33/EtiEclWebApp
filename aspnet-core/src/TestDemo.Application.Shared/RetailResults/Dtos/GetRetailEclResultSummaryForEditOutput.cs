using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailResults.Dtos
{
    public class GetRetailEclResultSummaryForEditOutput
    {
		public CreateOrEditRetailEclResultSummaryDto RetailEclResultSummary { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}