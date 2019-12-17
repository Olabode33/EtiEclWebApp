using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.ObeAssumption.Dtos
{
    public class ObeEclPdAssumptionNplIndexDto : EntityDto<Guid>
    {

		 public Guid ObeEclId { get; set; }

		 
    }
}