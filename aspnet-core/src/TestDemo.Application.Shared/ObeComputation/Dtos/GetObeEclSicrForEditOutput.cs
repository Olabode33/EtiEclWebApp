using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetObeEclSicrForEditOutput
    {
		public CreateOrEditObeEclSicrDto ObeEclSicr { get; set; }

		public string ObeEclDataLoanBookCustomerName { get; set;}


    }
}