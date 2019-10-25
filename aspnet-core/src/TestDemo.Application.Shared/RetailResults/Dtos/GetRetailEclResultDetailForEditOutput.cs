using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailResults.Dtos
{
    public class GetRetailEclResultDetailForEditOutput
    {
		public CreateOrEditRetailEclResultDetailDto RetailEclResultDetail { get; set; }

		public string RetailEclTenantId { get; set;}

		public string RetailEclDataLoanBookCustomerName { get; set;}


    }
}