using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Importing.Assumptions.Dto
{
    public class ImportNplDataDto
    {
        public int? KeyRow { get; set; }
        public DateTime? Date { get; set; }
        public double? Actual { get; set; }
        public double? Standardised { get; set; }
        public double? EtiNplSeries { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}
