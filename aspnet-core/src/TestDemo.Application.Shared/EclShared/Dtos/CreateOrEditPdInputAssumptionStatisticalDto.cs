﻿using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclShared.Dtos
{
    public class CreateOrEditPdInputAssumptionStatisticalDto : EntityDto<Guid?>
    {
        public double Value { get; set; }
        public bool CanAffiliateEdit { get; set; }

    }
}