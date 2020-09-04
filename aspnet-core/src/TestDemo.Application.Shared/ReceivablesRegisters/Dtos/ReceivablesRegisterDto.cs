using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.ReceivablesRegisters.Dtos
{
    public class ReceivablesRegisterDto : EntityDto<Guid>
    {
		public CalibrationStatusEnum Status { get; set; }



    }
}