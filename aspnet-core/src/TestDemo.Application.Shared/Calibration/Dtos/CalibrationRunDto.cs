using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.Calibration.Dtos
{
    public class CalibrationRunDto : EntityDto<Guid>
    {
        public DateTime? ClosedDate { get; set; }
        public CalibrationStatusEnum Status { get; set; }
        public FrameworkEnum ModelType { get; set; }
        public long? CloseByUserId { get; set; }
    }

    public class MacroAnalysisRunDto : EntityDto
    {
        public DateTime? ClosedDate { get; set; }
        public CalibrationStatusEnum Status { get; set; }
        public FrameworkEnum ModelType { get; set; }
        public long? CloseByUserId { get; set; }
    }
}