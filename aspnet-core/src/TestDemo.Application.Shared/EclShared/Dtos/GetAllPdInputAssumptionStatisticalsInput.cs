using Abp.Application.Services.Dto;
using System;

namespace TestDemo.EclShared.Dtos
{
    public class GetAllPdInputAssumptionStatisticalsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int CanAffiliateEditFilter { get; set; }



    }
}