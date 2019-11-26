
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.ObeComputation.Dtos
{
    public class ObePdLifetimeBestDto : EntityDto<Guid>
    {

		 public Guid ObeEclId { get; set; }

		 
    }
}