using Abp.Application.Services.Dto;
using System;

namespace TestDemo.IVModels.Dtos
{
    public class GetAllHoldCoRegistersInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? StatusFilter { get; set; }



    }
}