using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetObeEclOverrideForEditOutput
    {
		public CreateOrEditObeEclOverrideDto ObeEclOverride { get; set; }

		public string ObeEclDataLoanBookCustomerName { get; set;}


    }
}