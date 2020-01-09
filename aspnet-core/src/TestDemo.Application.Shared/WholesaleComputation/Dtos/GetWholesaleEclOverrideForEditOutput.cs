using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetWholesaleEclOverrideForEditOutput
    {
		public CreateOrEditWholesaleEclOverrideDto WholesaleEclOverride { get; set; }

		public string WholesaleEclDataLoanBookCustomerName { get; set;}


    }
}