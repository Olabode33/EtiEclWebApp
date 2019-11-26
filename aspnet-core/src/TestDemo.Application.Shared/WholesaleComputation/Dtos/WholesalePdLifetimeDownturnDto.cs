
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class WholesalePdLifetimeDownturnDto : EntityDto<Guid>
    {

		 public Guid? WholesaleEclId { get; set; }

		 
    }
}