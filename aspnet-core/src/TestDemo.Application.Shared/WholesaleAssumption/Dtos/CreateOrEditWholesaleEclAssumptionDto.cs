using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.Dto;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class CreateOrEditWholesaleEclAssumptionDto : CreateOrEditEclFrameworkAssumptionBase
	{
		 public Guid WholesaleEclId { get; set; }	 
    }
}