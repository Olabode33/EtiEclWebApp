using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.EclShared.Dtos
{
    public class PdInputAssumptionStatisticalDto : EntityDto<Guid>
    {
		public bool CanAffiliateEdit { get; set; }



    }
}