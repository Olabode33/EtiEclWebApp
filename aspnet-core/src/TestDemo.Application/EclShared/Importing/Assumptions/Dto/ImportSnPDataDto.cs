using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Importing.Assumptions.Dto
{
    public class ImportSnPDataDto
    {
        public virtual string Rating { get; set; }

        public virtual int? Years { get; set; }

        public virtual double? Value { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}
