﻿using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.InvestmentComputation.Dtos
{
    public class ImportExcelInvestmentEclOverrideDto : EntityDto<Guid>
    {
		public int? StageOverride { get; set; }

		public string AssetDescription { get; set; }

		public double? ImpairmentOverride { get; set; }

		public string OverrideComment { get; set; }

		public string OverrideType { get; set; }

		public GeneralStatusEnum Status { get; set; }

		public Guid EclId { get; set; }

		public Guid InvestmentEclSicrId { get; set; }

		 
    }
}