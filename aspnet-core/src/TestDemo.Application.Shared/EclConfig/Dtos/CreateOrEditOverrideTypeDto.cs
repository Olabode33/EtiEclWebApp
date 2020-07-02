
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclConfig.Dtos
{
    public class CreateOrEditOverrideTypeDto : EntityDto<int?>
    {

		public string Name { get; set; }
		
		
		public string Description { get; set; }
		
		

    }
}