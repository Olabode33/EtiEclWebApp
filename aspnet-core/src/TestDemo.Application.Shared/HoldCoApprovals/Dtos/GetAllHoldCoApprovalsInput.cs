using Abp.Application.Services.Dto;
using System;

namespace TestDemo.HoldCoApprovals.Dtos
{
    public class GetAllHoldCoApprovalsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}