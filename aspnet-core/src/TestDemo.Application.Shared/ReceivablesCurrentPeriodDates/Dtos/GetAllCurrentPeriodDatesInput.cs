using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ReceivablesCurrentPeriodDates.Dtos
{
    public class GetAllCurrentPeriodDatesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}