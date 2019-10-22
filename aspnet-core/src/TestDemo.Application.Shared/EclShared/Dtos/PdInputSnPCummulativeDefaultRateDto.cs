
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.EclShared.Dtos
{
    public class PdInputSnPCummulativeDefaultRateDto : EntityDto<Guid>
    {
		public string Rating { get; set; }

		public int? Years { get; set; }

		public double? Value { get; set; }



    }
}