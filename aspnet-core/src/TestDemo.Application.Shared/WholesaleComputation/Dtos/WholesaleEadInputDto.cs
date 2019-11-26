
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class WholesaleEadInputDto : EntityDto<Guid>
    {

		 public Guid WholesaleEclId { get; set; }

		 
    }
}