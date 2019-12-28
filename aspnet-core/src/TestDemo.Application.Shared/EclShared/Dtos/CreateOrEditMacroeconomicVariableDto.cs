
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclShared.Dtos
{
    public class CreateOrEditMacroeconomicVariableDto : EntityDto<int?>
    {

		public string Name { get; set; }
		
		
		public string Description { get; set; }
		
		

    }
}