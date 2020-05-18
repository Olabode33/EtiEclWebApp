using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.Organizations.Dto;

namespace TestDemo.EclConfig.Dtos
{
    public class AffiliateConfigurationDto: EntityDto<long>
    {
        public string AffiliateName { get; set; }
        public string Code { get; set; }
        public double? OverrideThreshold { get; set; }
        public string Currency { get; set; }
    }
}
