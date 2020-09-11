using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.LoanImpairmentsRegisters.Dtos
{
    public class LoanImpairmentRegisterDto : EntityDto<Guid>
    {
		public CalibrationStatusEnum Status { get; set; }



    }
}