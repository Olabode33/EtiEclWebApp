
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.RetailComputation.Dtos
{
    public class RetailPdRedefaultLifetimeOptimisticDto : EntityDto<Guid>
    {

		 public Guid RetailEclId { get; set; }

		 
    }
}