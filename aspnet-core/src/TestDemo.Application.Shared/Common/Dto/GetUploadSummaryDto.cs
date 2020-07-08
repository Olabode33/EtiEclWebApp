using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Common.Dto
{
    public class GetCalibrationUploadSummaryDto
    {
        public Guid RegisterId { get; set; }
        public int AllJobs { get; set; }
        public int CompletedJobs { get; set; }
        public GeneralStatusEnum Status { get; set; }
        public string Comment { get; set; }
    }

    public class GetMacroUploadSummaryDto
    {
        public int RegisterId { get; set; }
        public int AllJobs { get; set; }
        public int CompletedJobs { get; set; }
        public GeneralStatusEnum Status { get; set; }
        public string Comment { get; set; }
    }
}
