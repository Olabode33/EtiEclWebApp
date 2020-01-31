using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclConfig.Dtos
{
    public class GetAffiliateOverrideThresholdForEditOutput
    {
		public CreateOrEditAffiliateDto AffiliateOverrideThreshold { get; set; }

		public string OrganizationUnitDisplayName { get; set;}


    }
}