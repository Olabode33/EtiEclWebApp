using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.CalibrationRunBase;

namespace TestDemo.Calibration.Approvals
{
    [Table("CalibrationLgdRecoveryRateApprovals")]
    public class CalibrationLgdRecoveryRateApproval : CalibrationRunApprovalBase
    {
        [ForeignKey("CalibrationId")]
        public CalibrationLgdRecoveryRate CalibrationFk { get; set; }
    }
}
