using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetWholesaleEadCirProjectionForEditOutput
    {
		public CreateOrEditWholesaleEadCirProjectionDto WholesaleEadCirProjection { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}