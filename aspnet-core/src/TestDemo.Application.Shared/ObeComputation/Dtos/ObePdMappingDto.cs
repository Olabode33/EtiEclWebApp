
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.ObeComputation.Dtos
{
    public class ObePdMappingDto : EntityDto<Guid>
    {

		 public Guid? ObeEclId { get; set; }

		 
    }
}