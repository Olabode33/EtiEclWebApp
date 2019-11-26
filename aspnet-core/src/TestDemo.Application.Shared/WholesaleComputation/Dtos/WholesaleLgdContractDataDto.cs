
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class WholesaleLgdContractDataDto : EntityDto<Guid>
    {

		 public Guid WholesaleEclId { get; set; }

		 
    }
}