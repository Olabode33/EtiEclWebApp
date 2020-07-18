using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Dtos
{
    public class GetAllEclForWorkspaceDto: EntityDto<Guid?>
    {
        public FrameworkEnum Framework { get; set; }
        public DateTime ReportingDate { get; set; }
        public EclStatusEnum Status { get; set; }
        public string OrganisationUnitName { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? IsSingleBatch { get; set; }
    }

    public class GetAllEclForWorkspaceSummaryDto: GetAllEclForWorkspaceDto
    {
        public DateTime? LastUpdated { get; set; }
    }
}
