using Abp.Application.Services.Dto;
using System;

namespace TestDemo.EclShared.Dtos
{
    public class GetAllPdInputAssumptionsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}