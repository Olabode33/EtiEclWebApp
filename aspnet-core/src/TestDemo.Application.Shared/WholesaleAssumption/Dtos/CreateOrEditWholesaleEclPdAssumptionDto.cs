using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class CreateOrEditWholesaleEclPdAssumptionDto : EntityDto<Guid?>
    {

		 public Guid WholesaleEclId { get; set; }
		 
		 
    }
}