using Abp.Application.Services.Dto;
using System;

namespace TestDemo.EclShared.Dtos
{
    public class GetAllAssumptionApprovalsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public long? OrganizationUnitIdFilter { get; set; }

		public int FrameworkFilter { get; set; }

		public int AssumptionTypeFilter { get; set; }

		public string AssumptionGroupFilter { get; set; }


		 public string UserNameFilter { get; set; }

		 
    }
}