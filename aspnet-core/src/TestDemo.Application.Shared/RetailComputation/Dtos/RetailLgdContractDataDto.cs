
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.RetailComputation.Dtos
{
    public class RetailLgdContractDataDto : EntityDto<Guid>
    {
		public string CONTRACT_NO { get; set; }


		 public Guid RetailEclId { get; set; }

		 
    }
}