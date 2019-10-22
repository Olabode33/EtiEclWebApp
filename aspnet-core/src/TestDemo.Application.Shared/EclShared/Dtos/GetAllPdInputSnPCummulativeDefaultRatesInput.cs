using Abp.Application.Services.Dto;
using System;

namespace TestDemo.EclShared.Dtos
{
    public class GetAllPdInputSnPCummulativeDefaultRatesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string RatingFilter { get; set; }



    }
}