using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace TestDemo.EclShared.Dtos
{
    public class AssumptionApprovalDto : EntityDto<Guid>
    {
        public long OrganizationUnitId { get; set; }

        public FrameworkEnum Framework { get; set; }

        public AssumptionTypeEnum AssumptionType { get; set; }

        public string AssumptionGroup { get; set; }

        public string InputName { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public DateTime? DateReviewed { get; set; }

        public GeneralStatusEnum Status { get; set; }

        public string ReviewComment { get; set; }

        public long? ReviewedByUserId { get; set; }
        public Guid AssumptionId { get; set; }

        public string AssumptionEntity { get; set; }

    }

    public class ReviewMultipleRecordsDto<T>
    {
        public string ReviewComment { get; set; }
        public List<T> Items { get; set; }
    }
}