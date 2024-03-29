﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.InvestmentComputation.Dtos
{
    public class GetInvestmentPreResultForOverrideOutput
    {
        public CreateOrEditEclOverrideDto EclOverrides { get; set; }
        public string AssetDescription { get; set; }
        public string AssetType { get; set; }
        public string CurrentRating { get; set; }
        public int Stage { get; set; }
        public double? Impairment { get; set; }
        public double? Outstanding_Balance { get; set; }
    }
}
