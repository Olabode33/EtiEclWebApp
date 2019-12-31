using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclShared.Dtos
{
    public class CreateOrEditPdInputAssumptionNplIndexDto : EntityDto<Guid?>
    {

        public double Actual { get; set; }

        public double Standardised { get; set; }

        public double EtiNplSeries { get; set; }
    }
}