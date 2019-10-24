using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleInputs.Dtos
{
    public class GetWholesaleEclDataLoanBookForEditOutput
    {
		public CreateOrEditWholesaleEclDataLoanBookDto WholesaleEclDataLoanBook { get; set; }

		public string WholesaleEclUploadUploadComment { get; set;}


    }
}