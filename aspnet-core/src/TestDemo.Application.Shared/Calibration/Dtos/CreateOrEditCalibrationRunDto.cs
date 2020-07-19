using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.Calibration.Dtos
{
    public class CreateOrEditCalibrationRunDto : EntityDto<Guid?>
    {
        public CalibrationStatusEnum Status { get; set; }
        public long? CloseByUserId { get; set; }
        public long? AffiliateId { get; set; }
        public FrameworkEnum ModelType { get; set; }
        public  string FriendlyException { get; set; }
    }

    public class CreateOrEditMacroAnalysisRunDto : EntityDto<int?>
    {
        public CalibrationStatusEnum Status { get; set; }
        public long? CloseByUserId { get; set; }
        public long? AffiliateId { get; set; }
        public FrameworkEnum ModelType { get; set; }
        public string FriendlyException { get; set; }
    }
}