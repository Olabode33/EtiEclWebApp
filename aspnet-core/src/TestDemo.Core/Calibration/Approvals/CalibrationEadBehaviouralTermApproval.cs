using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.CalibrationRunBase;

namespace TestDemo.Calibration.Approvals
{
    [Table("CalibrationEadBehaviouralTermApprovals")]
    public class CalibrationEadBehaviouralTermApproval: CalibrationRunApprovalBase
    {
        [ForeignKey("CalibrationId")]
        public CalibrationEadBehaviouralTerm CalibrationFk { get; set; }
    }
}
