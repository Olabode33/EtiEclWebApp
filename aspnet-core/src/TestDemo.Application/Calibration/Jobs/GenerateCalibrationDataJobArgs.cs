using Abp;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;
using TestDemo.WholesaleInputs;

namespace TestDemo.Calibration.Jobs
{
    [Serializable]
    public class GenerateCalibrationDataJobArgs
    {
        public List<WholesaleEclDataLoanBook> LoanbookData { get; set; }
    }

  
}
