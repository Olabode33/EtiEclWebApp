using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Importing.Assumptions.Dto
{
    public class ImportNonInternalModelDataDto
    {
        public int? Month { get; set; }
        public string PdGroup { get; set; }
        public double? MarginalDefaultRate { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}
