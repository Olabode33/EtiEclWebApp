using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Importing.Assumptions.Dto
{
    public class ImportMacroProjectionDataDto
    {
        public virtual DateTime? Date { get; set; }
        public virtual string InputName { get; set; }
        public virtual double? BestValue { get; set; }
        public virtual double? OptimisticValue { get; set; }
        public virtual double? DownturnValue { get; set; }
        public virtual int? MacroeconomicVariableId { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}
