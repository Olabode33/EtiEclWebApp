using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class CreateOrEditWholesalePdRedefaultLifetimeDto : EntityDto<Guid?>
    {

		 public Guid? WholesaleEclId { get; set; }
		 
		 
    }
}