using Abp.Application.Services.Dto;
using System;

namespace TestDemo.EclShared.Dtos
{
    public class GetAllInvSecFitchCummulativeDefaultRatesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int StatusFilter { get; set; }



    }
}