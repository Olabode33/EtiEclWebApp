using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.CalibrationResult
{
    [Table("CalibrationResult_EAD_Behavioural_Terms")]
    public class CalibrationResultEadBehaviouralTerms: EntityDto
    {
        public virtual string Assumption_NonExpired { get; set; }
        public virtual string Freq_NonExpired { get; set; }
        public virtual string Assumption_Expired { get; set; }
        public virtual string Freq_Expired { get; set; }
        public virtual string Comment { get; set; }
        public virtual int Status { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual Guid? CalibrationId { get; set; }
    }
}
