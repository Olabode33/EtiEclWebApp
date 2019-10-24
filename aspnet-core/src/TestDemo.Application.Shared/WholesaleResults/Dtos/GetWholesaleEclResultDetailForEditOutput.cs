using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleResults.Dtos
{
    public class GetWholesaleEclResultDetailForEditOutput
    {
		public CreateOrEditWholesaleEclResultDetailDto WholesaleEclResultDetail { get; set; }

		public string WholesaleEclTenantId { get; set;}

		public string WholesaleEclDataLoanBookCustomerName { get; set;}


    }
}