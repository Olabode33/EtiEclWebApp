using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.EclShared.Dtos
{
    public class LgdAssumptionUnsecuredRecoveryDto : EntityDto<Guid>
    {
		public string InputName { get; set; }

		public string Value { get; set; }



    }
}