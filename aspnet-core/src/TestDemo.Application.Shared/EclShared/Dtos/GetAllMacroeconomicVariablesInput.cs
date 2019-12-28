using Abp.Application.Services.Dto;
using System;

namespace TestDemo.EclShared.Dtos
{
    public class GetAllMacroeconomicVariablesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}