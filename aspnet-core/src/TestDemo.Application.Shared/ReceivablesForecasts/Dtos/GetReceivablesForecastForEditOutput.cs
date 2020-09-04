using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ReceivablesForecasts.Dtos
{
    public class GetReceivablesForecastForEditOutput
    {
		public CreateOrEditReceivablesForecastDto ReceivablesForecast { get; set; }


    }
}