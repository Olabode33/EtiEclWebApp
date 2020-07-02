using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.Workers.Trackers
{
    public class TrackCalibrationUploadSummary : Entity
    {
        public Guid RegisterId { get; set; }
        public int AllJobs { get; set; }
        public int CompletedJobs { get; set; }
    }
}
