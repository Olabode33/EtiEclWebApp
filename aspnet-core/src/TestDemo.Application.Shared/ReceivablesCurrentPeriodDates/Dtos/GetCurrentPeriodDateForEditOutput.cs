using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ReceivablesCurrentPeriodDates.Dtos
{
    public class GetCurrentPeriodDateForEditOutput
    {
		public CreateOrEditCurrentPeriodDateDto CurrentPeriodDate { get; set; }


    }
}