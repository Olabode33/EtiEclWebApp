using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetWholesalePdAssumptionNonInternalModelForEditOutput
    {
		public CreateOrEditWholesalePdAssumptionNonInternalModelDto WholesalePdAssumptionNonInternalModel { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}