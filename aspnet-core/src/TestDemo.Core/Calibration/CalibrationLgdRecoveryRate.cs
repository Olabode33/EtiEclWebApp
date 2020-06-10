using Abp.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.CalibrationRunBase;

namespace TestDemo.Calibration
{
    [Table("CalibrationRunLgdRecoveryRate")]
    [Audited]
    public class CalibrationLgdRecoveryRate: CalibrationRunBase
    {
    }
}
