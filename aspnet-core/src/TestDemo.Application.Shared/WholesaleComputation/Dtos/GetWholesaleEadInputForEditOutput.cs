using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetWholesaleEadInputForEditOutput
    {
		public CreateOrEditWholesaleEadInputDto WholesaleEadInput { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}