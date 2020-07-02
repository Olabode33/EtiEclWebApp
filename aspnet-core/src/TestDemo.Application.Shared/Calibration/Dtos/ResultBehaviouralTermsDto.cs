using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Calibration.Dtos
{
    public class ResultBehaviouralTermsDto: EntityDto {

        public string Assumption_NonExpired { get; set; }
        public string Freq_NonExpired { get; set; }
        public string Assumption_Expired { get; set; }
        public string Freq_Expired { get; set; }
        public string Comment { get; set; }
        public int Status { get; set; }
        public int Id { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual Guid? CalibrationId { get; set; }

    }
}
