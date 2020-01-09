using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetRetailEclOverrideForEditOutput
    {
		public CreateOrEditRetailEclOverrideDto RetailEclOverride { get; set; }

		public string RetailEclDataLoanBookCustomerName { get; set;}


    }
}