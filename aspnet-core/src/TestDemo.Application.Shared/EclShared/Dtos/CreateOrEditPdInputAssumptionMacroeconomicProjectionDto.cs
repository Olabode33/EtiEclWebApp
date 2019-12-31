using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclShared.Dtos
{
    public class CreateOrEditPdInputAssumptionMacroeconomicProjectionDto : EntityDto<Guid?>
    {
        public double BestValue { get; set; }

        public double OptimisticValue { get; set; }

        public double DownturnValue { get; set; }
    }
}