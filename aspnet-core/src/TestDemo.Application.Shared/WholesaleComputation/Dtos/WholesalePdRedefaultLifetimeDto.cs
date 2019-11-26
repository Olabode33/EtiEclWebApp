using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class WholesalePdRedefaultLifetimeDto : EntityDto<Guid>
    {

		 public Guid? WholesaleEclId { get; set; }

		 
    }
}