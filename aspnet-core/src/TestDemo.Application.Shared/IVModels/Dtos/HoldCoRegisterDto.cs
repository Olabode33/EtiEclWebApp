using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.IVModels.Dtos
{
    public class HoldCoRegisterDto : EntityDto<Guid>
    {
		public CalibrationStatusEnum Status { get; set; }



    }
}