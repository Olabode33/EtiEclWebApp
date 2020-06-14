using Abp.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.CalibrationRunBase;

namespace TestDemo.Calibration
{
    [Table("CalibrationRunMacroAnalysis")]
    [Audited]
    public class MacroAnalysis: CalibrationRunBase
    {
    }
}
