using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailResults.Dtos
{
    public class GetRetailEclResultSummaryKeyInputForEditOutput
    {
		public CreateOrEditRetailEclResultSummaryKeyInputDto RetailEclResultSummaryKeyInput { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}