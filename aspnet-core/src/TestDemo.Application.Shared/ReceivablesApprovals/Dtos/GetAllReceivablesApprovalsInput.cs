using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ReceivablesApprovals.Dtos
{
    public class GetAllReceivablesApprovalsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}