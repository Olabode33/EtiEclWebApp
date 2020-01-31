
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclConfig.Dtos
{
    public class CreateOrEditAffiliateDto : EntityDto<int?>
    {
        public long? ParentId { get; set; }
		public string DisplayName { get; set; }
		public double OverrideThreshold { get; set; }		 
    }
}