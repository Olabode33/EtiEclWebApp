using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Common.Dto
{
    public class GetFacilityStageTrackerInputDto
    {
        public string Facility { get; set; }
        public FrameworkEnum Framework { get; set; }
        public Guid EclId { get; set; }
    }

    public class FacilityStageTrackerOutputDto
    {
        public string Facility { get; set; }
        public int? Stage { get; set; }
        public DateTime? LastReportingDate { get; set; }
        public FrameworkEnum Framework { get; set; }
        public long OrganizationUnitId { get; set; }
    }
}
