
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class WholesalePdRedefaultLifetimeOptimisticDto : EntityDto<Guid>
    {

		 public Guid? WholesaleEclId { get; set; }

		 
    }
}