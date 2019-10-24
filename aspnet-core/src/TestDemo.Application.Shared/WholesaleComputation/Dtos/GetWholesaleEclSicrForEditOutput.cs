using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetWholesaleEclSicrForEditOutput
    {
		public CreateOrEditWholesaleEclSicrDto WholesaleEclSicr { get; set; }

		public string WholesaleEclDataLoanBookContractNo { get; set;}


    }
}