using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.RetailAssumption.Dtos
{
    public class RetailEclPdAssumptionDto : EntityDto<Guid>
    {

		 public Guid RetailEclId { get; set; }

		 
    }
}