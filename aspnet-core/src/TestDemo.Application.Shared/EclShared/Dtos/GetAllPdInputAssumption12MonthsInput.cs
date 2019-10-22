using Abp.Application.Services.Dto;
using System;

namespace TestDemo.EclShared.Dtos
{
    public class GetAllPdInputAssumption12MonthsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxCreditFilter { get; set; }
		public int? MinCreditFilter { get; set; }

		public string SnPMappingEtiCreditPolicyFilter { get; set; }



    }
}