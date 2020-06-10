using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.CalibrationRunBase;

namespace TestDemo.Calibration.Approvals
{
    [Table("CalibrationPdCrDrApprovals")]
    public class CalibrationPdCrDrApproval : CalibrationRunApprovalBase
    {
        [ForeignKey("CalibrationId")]
        public CalibrationPdCrDr CalibrationFk { get; set; }
    }
}
