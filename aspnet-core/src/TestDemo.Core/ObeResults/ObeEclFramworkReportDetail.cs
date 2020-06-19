using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.FinalReportBase;

namespace TestDemo.ObeResults
{
    [Table("ObeEclFramworkReportDetail")]
    public class ObeEclFramworkReportDetail: FrameworkReportDetailBase
    {
        public Guid? ObeEclId { get; set; }
    }
}
