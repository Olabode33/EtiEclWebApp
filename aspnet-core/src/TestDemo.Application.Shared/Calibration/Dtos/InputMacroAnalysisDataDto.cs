using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Importing.Calibration.Dto
{
    public class InputMacroAnalysisDataDto : EntityDto
    {
        public int? MacroeconomicId { get; set; }
        public double? Value { get; set; }
        public DateTime? Period { get; set; }
        //public double? NPL_Percentage_Ratio { get; set; }
        public long? AffiliateId { get; set; }
        public int? MacroId { get; set; }
        public string MacroeconomicVariableName { get; set; }
    }

    public class GetMacroAnalysisDataDto
    {
        public List<string> Columns { get; set; } 
        public List<List<double?>> Values { get; set; } 
        public List<DateTime?> Periods { get; set; }
    }
}
