using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeInputs.Dtos
{
    public class GetObeEclDataLoanBookForEditOutput
    {
		public CreateOrEditObeEclDataLoanBookDto ObeEclDataLoanBook { get; set; }

		public string ObeEclUploadTenantId { get; set;}


    }
}