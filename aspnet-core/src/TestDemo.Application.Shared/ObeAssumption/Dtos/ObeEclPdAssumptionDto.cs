using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.ObeAssumption.Dtos
{
    public class ObeEclPdAssumptionDto : EntityDto<Guid>
    {

		 public Guid ObeEclId { get; set; }

		 
    }
}