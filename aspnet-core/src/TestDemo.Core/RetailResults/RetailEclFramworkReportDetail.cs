using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.FinalReportBase;

namespace TestDemo.RetailResults
{
    [Table("RetailEclFramworkReportDetail")]
    public class RetailEclFramworkReportDetail: FrameworkReportDetailBase
    {
        public Guid? RetailEclId { get; set; }
    }
}
