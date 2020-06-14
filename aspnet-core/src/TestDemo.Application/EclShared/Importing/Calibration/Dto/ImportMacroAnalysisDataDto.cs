using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Importing.Calibration.Dto
{
    public class ImportMacroAnalysisDataDto
    {
        public int? MacroeconomicId { get; set; }
        public double? Value { get; set; }
        public DateTime? Period { get; set; }
        //public double? NPL_Percentage_Ratio { get; set; }
        public long? AffiliateId { get; set; }
        public int? MacroId { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}
