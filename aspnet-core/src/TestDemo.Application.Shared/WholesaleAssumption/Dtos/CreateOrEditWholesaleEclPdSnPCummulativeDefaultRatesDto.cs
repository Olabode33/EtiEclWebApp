
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.Dto.Assumptions;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesDto : CreateOrEditEclPdSnPCummulativeDefaultRateDtoBase
	{
		 public Guid? WholesaleEclId { get; set; }
    }
}