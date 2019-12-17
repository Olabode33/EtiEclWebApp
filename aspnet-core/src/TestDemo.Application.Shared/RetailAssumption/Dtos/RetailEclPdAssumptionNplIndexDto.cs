using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.RetailAssumption.Dtos
{
    public class RetailEclPdAssumptionNplIndexDto : EntityDto<Guid>
    {

		 public Guid RetailEclId { get; set; }

		 
    }
}