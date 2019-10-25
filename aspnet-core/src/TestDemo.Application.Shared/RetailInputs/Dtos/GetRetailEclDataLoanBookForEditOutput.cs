using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailInputs.Dtos
{
    public class GetRetailEclDataLoanBookForEditOutput
    {
		public CreateOrEditRetailEclDataLoanBookDto RetailEclDataLoanBook { get; set; }

		public string RetailEclUploadTenantId { get; set;}


    }
}