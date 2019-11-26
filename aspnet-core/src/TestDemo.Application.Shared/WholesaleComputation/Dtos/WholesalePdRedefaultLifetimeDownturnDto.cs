
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class WholesalePdRedefaultLifetimeDownturnDto : EntityDto<Guid>
    {

		 public Guid? WholesaleEclId { get; set; }

		 
    }
}