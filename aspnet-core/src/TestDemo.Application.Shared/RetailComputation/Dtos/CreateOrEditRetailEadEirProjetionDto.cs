
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class CreateOrEditRetailEadEirProjetionDto : EntityDto<Guid?>
    {

		 public Guid RetailEclId { get; set; }
		 
		 
    }
}