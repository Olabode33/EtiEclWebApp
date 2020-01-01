using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclShared.Dtos
{
    public class CreateOrEditAssumptionApprovalDto : EntityDto<Guid?>
    {

		public long OrganizationUnitId { get; set; }
		
		
		public FrameworkEnum Framework { get; set; }
		
		
		public AssumptionTypeEnum AssumptionType { get; set; }
		
		
		public string AssumptionGroup { get; set; }
		
		
		public string InputName { get; set; }
		
		
		public string OldValue { get; set; }
		
		
		public string NewValue { get; set; }
		
		
		public DateTime? DateReviewed { get; set; }
		
		
		public string ReviewComment { get; set; }
		
		
		public GeneralStatusEnum Status { get; set; }
		
		
		 public long? ReviewedByUserId { get; set; }

        public Guid AssumptionId { get; set; }

        public string AssumptionEntity { get; set; }

    }
}