using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailResults.Dtos
{
    public class GetRetailEclResultSummaryTopExposureForEditOutput
    {
		public CreateOrEditRetailEclResultSummaryTopExposureDto RetailEclResultSummaryTopExposure { get; set; }

		public string RetailEclTenantId { get; set;}

		public string RetailEclDataLoanBookContractNo { get; set;}


    }
}