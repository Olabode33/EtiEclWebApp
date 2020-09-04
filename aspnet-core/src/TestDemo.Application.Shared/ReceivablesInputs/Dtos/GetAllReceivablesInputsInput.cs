using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ReceivablesInputs.Dtos
{
    public class GetAllReceivablesInputsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}