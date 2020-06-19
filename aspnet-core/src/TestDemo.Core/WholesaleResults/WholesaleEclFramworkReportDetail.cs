using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.FinalReportBase;

namespace TestDemo.WholesaleResults
{
    [Table("WholesaleEclFramworkReportDetail")]
    public class WholesaleEclFramworkReportDetail: FrameworkReportDetailBase
    {
        public Guid? WholesaleEclId { get; set; }
    }
}
