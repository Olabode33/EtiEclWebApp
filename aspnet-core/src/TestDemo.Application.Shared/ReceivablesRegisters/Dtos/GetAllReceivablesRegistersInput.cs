using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ReceivablesRegisters.Dtos
{
    public class GetAllReceivablesRegistersInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? StatusFilter { get; set; }



    }
}