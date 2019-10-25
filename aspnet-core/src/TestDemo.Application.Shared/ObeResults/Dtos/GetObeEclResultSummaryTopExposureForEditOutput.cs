using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeResults.Dtos
{
    public class GetObeEclResultSummaryTopExposureForEditOutput
    {
		public CreateOrEditObeEclResultSummaryTopExposureDto ObeEclResultSummaryTopExposure { get; set; }

		public string ObeEclTenantId { get; set;}

		public string ObeEclDataLoanBookCustomerName { get; set;}


    }
}