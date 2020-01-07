using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclConfig.Dtos
{
    public class CreateOrEditEclConfigurationDto : EntityDto<int?>
    {

		public string PropertyKey { get; set; }
		
		
		public string DisplayName { get; set; }
		
		
		public string Value { get; set; }
		
		
		public DataTypeEnum DataType { get; set; }
		
		

    }
}