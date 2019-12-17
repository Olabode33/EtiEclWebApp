using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class WholesaleEclPdAssumptionNplIndexDto : EntityDto<Guid>
    {

		 public Guid WholesaleEclId { get; set; }

		 
    }
}