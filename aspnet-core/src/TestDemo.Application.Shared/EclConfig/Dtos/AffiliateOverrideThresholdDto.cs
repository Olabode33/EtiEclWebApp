
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.EclConfig.Dtos
{
    public class AffiliateOverrideThresholdDto : EntityDto
    {
		public double Threshold { get; set; }


		 public long OrganizationUnitId { get; set; }

		 
    }
}