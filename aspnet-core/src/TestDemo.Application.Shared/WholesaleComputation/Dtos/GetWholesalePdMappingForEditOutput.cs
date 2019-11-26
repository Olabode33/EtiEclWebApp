using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetWholesalePdMappingForEditOutput
    {
		public CreateOrEditWholesalePdMappingDto WholesalePdMapping { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}