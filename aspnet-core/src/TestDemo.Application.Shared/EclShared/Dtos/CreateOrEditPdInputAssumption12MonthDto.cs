
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclShared.Dtos
{
    public class CreateOrEditPdInputAssumption12MonthDto : EntityDto<Guid?>
    {

		public int Credit { get; set; }
		
		
		public double? PD { get; set; }
		
		
		public string SnPMappingEtiCreditPolicy { get; set; }
		
		
		public string SnPMappingBestFit { get; set; }
		
		

    }
}