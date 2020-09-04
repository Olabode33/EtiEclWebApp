using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ReceivablesResults.Dtos
{
    public class GetAllReceivablesResultsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}