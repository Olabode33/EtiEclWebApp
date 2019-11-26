using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetWholesalePdLifetimeBestForEditOutput
    {
		public CreateOrEditWholesalePdLifetimeBestDto WholesalePdLifetimeBest { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}