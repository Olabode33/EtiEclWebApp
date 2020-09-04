
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ReceivablesForecasts.Dtos
{
    public class InputReceivablesForecastDto : EntityDto<Guid?>
    {

		public string Period { get; set; }
		
		
		public double Optimistic { get; set; }
		
		
		public double Base { get; set; }
		
		
		public double Downturn { get; set; }
		

    }
}