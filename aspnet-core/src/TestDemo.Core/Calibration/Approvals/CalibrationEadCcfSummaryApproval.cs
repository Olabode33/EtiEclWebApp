using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.CalibrationRunBase;

namespace TestDemo.Calibration.Approvals
{
    [Table("CalibrationEadCcfSummaryApprovals")]
    public class CalibrationEadCcfSummaryApproval : CalibrationRunApprovalBase
    {
        [ForeignKey("CalibrationId")]
        public CalibrationEadCcfSummary CalibrationFk { get; set; }
    }
}
