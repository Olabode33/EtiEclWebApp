using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetRetailEclSicrForEditOutput
    {
		public CreateOrEditRetailEclSicrDto RetailEclSicr { get; set; }

		public string RetailEclDataLoanBookCustomerName { get; set;}


    }
}