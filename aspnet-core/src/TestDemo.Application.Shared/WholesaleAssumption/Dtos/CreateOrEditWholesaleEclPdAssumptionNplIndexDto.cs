using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.Dto.Assumptions;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class CreateOrEditWholesaleEclPdAssumptionNplIndexDto : CreateOrEditEclPdAssumptionNplIndexDtoBase
    {
		 public Guid WholesaleEclId { get; set; }	 
    }
}