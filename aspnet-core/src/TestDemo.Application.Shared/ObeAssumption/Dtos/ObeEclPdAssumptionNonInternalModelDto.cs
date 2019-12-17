
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.ObeAssumption.Dtos
{
    public class ObeEclPdAssumptionNonInternalModelDto : EntityDto<Guid>
    {

		 public Guid ObeEclId { get; set; }

		 
    }
}