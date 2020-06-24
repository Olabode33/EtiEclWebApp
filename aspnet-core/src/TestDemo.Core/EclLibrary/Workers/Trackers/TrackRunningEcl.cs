using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.Workers.Trackers
{
    public class TrackRunningGuidRegister: Entity
    {
        public Guid RegisterId { get; set; }
        public TrackTypeEnum Type { get; set; }
    }

    public class TrackRunningIntRegister : Entity
    {
        public int RegisterId { get; set; }
        public TrackTypeEnum Type { get; set; }
    }
}
