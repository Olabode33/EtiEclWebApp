
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class WholesalePdAssumptionNonInternalModelDto : EntityDto<Guid>
    {

		 public Guid WholesaleEclId { get; set; }

		 
    }
}