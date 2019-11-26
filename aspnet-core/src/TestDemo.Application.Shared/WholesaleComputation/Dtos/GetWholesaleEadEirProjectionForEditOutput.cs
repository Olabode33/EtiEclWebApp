using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetWholesaleEadEirProjectionForEditOutput
    {
		public CreateOrEditWholesaleEadEirProjectionDto WholesaleEadEirProjection { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}