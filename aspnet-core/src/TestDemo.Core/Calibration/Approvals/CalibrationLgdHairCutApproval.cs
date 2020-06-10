using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.CalibrationRunBase;

namespace TestDemo.Calibration.Approvals
{
    [Table("CalibrationLgdHairCutApprovals")]
    public class CalibrationLgdHairCutApproval: CalibrationRunApprovalBase
    {
        [ForeignKey("CalibrationId")]
        public CalibrationLgdHairCut CalibrationFk { get; set; }
    }
}
