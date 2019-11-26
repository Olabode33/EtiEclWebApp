
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleComputatoin.Dtos
{
    public class WholesalePdLifetimeOptimisticDto : EntityDto<Guid>
    {

		 public Guid? WholesaleEclId { get; set; }

		 
    }
}