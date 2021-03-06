﻿using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class CreateOrEditInvestmentEclPdFitchDefaultRateDto : EntityDto<Guid?>
    {
        public string Key { get; set; }
        public string Rating { get; set; }
        public int Year { get; set; }
        public double Value { get; set; }
        public bool RequiresGroupApproval { get; set; }
        public Guid InvestmentEclId { get; set; }
    }
}