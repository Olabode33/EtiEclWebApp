using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetObeEclPdAssumption12MonthForEditOutput
    {
		public CreateOrEditObeEclPdAssumption12MonthDto ObeEclPdAssumption12Month { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}