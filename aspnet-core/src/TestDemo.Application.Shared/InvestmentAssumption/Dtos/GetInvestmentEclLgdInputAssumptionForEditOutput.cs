﻿using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class GetInvestmentEclLgdInputAssumptionForEditOutput
    {
		public CreateOrEditInvestmentEclLgdInputAssumptionDto InvestmentEclLgdInputAssumption { get; set; }

		public string InvestmentEclReportingDate { get; set;}


    }
}