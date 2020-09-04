
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ReceivablesCurrentPeriodDates.Dtos
{
    public class InputCurrentPeriodDateDto : EntityDto<Guid?>
    {

		public string Account { get; set; }
		
		
		public double ZeroTo90 { get; set; }
		
		
		public double NinetyOneTo180 { get; set; }
		
		
		public double OneEightyOneTo365 { get; set; }
		
		
		public double Over365 { get; set; }
			
		

    }
}