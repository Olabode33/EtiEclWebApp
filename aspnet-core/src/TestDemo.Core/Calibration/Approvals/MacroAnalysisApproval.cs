using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.CalibrationRunBase;

namespace TestDemo.Calibration.Approvals
{
    [Table("MacroAnalysisApprovals")]
    public class MacroAnalysisApproval: CalibrationRunApprovalBase
    {
        [ForeignKey("CalibrationId")]
        public MacroAnalysis CalibrationFk { get; set; }
    }
}
