﻿using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.Workers.Trackers
{
    public class TrackRunningUploadJobs : Entity
    {
        public Guid RegisterId { get; set; }
    }
}
