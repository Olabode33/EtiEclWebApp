
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.EclShared.Dtos
{
    public class PdInputAssumption12MonthDto : EntityDto<Guid>
    {
		public int Credit { get; set; }

		public double? PD { get; set; }

		public string SnPMappingEtiCreditPolicy { get; set; }

		public string SnPMappingBestFit { get; set; }



    }
}