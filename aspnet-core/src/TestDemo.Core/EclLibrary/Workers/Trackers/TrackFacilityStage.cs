using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.Workers.Trackers
{
    public class TrackFacilityStage: Entity
    {
        public string Facility { get; set; }
        public int? Stage { get; set; }
        public DateTime? LastReportingDate { get; set; }
        public FrameworkEnum Framework { get; set; }
        public long OrganizationUnitId { get; set; }
    }
}
