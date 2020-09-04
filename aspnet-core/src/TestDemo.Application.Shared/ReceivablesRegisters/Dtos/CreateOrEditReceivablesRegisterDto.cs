using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.ReceivablesCurrentPeriodDates.Dtos;
using TestDemo.ReceivablesForecasts.Dtos;
using TestDemo.ReceivablesInputs.Dtos;
using System.Collections.Generic;

namespace TestDemo.ReceivablesRegisters.Dtos
{
    public class CreateOrEditReceivablesRegisterDto : EntityDto<Guid?>
    {

		public CalibrationStatusEnum Status { get; set; }


        public virtual CreateOrEditReceivablesInputDto InputParameter { get; set; }
        public virtual List<CreateOrEditCurrentPeriodDateDto> CurrentPeriodData { get; set; }
        public virtual List<CreateOrEditReceivablesForecastDto> ForecastData { get; set; }

    }
}