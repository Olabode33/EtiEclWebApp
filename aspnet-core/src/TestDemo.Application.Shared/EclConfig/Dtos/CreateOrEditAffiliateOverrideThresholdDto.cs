
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclConfig.Dtos
{
    public class CreateOrEditAffiliateOverrideThresholdDto : EntityDto<int?>
    {

		public double Threshold { get; set; }
		
		
		 public long OrganizationUnitId { get; set; }
		 
		 
    }
}