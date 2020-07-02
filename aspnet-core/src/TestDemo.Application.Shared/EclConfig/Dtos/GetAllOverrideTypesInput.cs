using Abp.Application.Services.Dto;
using System;

namespace TestDemo.EclConfig.Dtos
{
    public class GetAllOverrideTypesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}