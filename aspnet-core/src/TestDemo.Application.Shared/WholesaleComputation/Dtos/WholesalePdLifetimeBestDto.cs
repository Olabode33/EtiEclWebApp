using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class WholesalePdLifetimeBestDto : EntityDto<Guid>
    {

		 public Guid WholesaleEclId { get; set; }

		 
    }
}