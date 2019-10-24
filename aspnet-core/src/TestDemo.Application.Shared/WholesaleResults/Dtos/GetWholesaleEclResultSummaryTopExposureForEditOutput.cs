using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleResults.Dtos
{
    public class GetWholesaleEclResultSummaryTopExposureForEditOutput
    {
		public CreateOrEditWholesaleEclResultSummaryTopExposureDto WholesaleEclResultSummaryTopExposure { get; set; }

		public string WholesaleEclTenantId { get; set;}

		public string WholesaleEclDataLoanBookCustomerName { get; set;}


    }
}