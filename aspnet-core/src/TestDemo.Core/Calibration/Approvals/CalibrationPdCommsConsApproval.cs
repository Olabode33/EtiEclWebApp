using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.CalibrationRunBase;

namespace TestDemo.Calibration.Approvals
{
    [Table("CalibrationPdCommsConsApprovals")]
    public class CalibrationPdCommsConsApproval : CalibrationRunApprovalBase
    {
        [ForeignKey("CalibrationId")]
        public CalibrationPdCommsCons CalibrationFk { get; set; }
    }
}
