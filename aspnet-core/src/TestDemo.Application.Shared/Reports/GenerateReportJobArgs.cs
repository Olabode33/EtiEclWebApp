using Abp;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Reports
{
    [Serializable]
    public class GenerateReportJobArgs
    {
        public EclType eclType { get; set; }
        public Guid eclId { get; set; }
        public UserIdentifier userIdentifier { get; set; }
    }
}
