using Abp.Application.Services.Dto;
using System;

namespace TestDemo.IVModels.Dtos
{
    public class GetAllHoldCoInputParametersInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}