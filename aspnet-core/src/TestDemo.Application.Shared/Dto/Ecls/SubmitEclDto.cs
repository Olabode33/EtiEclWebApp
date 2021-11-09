using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Dto.Ecls
{
    public class SubmitEclDto : EntityDto<Guid>
    {
        public Guid? CalibrationEadBehaviouralTermId { get; set; }
        public Guid? CalibrationEadCcfSummaryId { get; set; }
        public Guid? CalibrationLgdHairCutId { get; set; }
        public Guid? CalibrationLgdRecoveryRateId { get; set; }
        public Guid? CalibrationPdCrDrId { get; set; }
        public Guid? CalibrationPdCommConsId { get; set; }
    }
}
