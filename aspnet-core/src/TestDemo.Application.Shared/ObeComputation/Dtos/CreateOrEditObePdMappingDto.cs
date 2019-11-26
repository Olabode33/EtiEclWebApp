
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class CreateOrEditObePdMappingDto : EntityDto<Guid?>
    {

		 public Guid? ObeEclId { get; set; }
		 
		 
    }
}