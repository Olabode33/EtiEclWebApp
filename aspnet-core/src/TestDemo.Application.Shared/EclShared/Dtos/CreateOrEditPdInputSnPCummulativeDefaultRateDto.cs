
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclShared.Dtos
{
    public class CreateOrEditPdInputSnPCummulativeDefaultRateDto : EntityDto<Guid?>
    {

		public string Key { get; set; }
		
		
		public string Rating { get; set; }
		
		
		public int? Years { get; set; }
		
		
		public double? Value { get; set; }
		
		

    }
}