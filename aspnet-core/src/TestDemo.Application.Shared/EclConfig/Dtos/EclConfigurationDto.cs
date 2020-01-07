using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.EclConfig.Dtos
{
    public class EclConfigurationDto : EntityDto
    {
		public string DisplayName { get; set; }

		public string Value { get; set; }

		public DataTypeEnum DataType { get; set; }



    }
}