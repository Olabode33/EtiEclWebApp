
using Abp;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.BaseEngine.Dtos
{
    [Serializable]
    public class RunEclJobArgs
    {
        public Guid EclId { get; set; }
        public UserIdentifier UserIdentifier { get; set; }
        public EclType EclType { get; set; }
    }

    [Serializable]
    public class UpdateFacilityStageTrackerJobArgs
    {
        public Guid EclId { get; set; }
        public long OrganizationUnitId { get; set; }
        public FrameworkEnum EclType { get; set; }
    }
}
