using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class CreateOrEditObeEclEadInputAssumptionDto : EntityDto<Guid?>
    {

		public string Key { get; set; }
		
		
		public string InputName { get; set; }
		
		
		public string Value { get; set; }
		
		
		public DataTypeEnum Datatype { get; set; }
		
		
		[Required]
		public bool IsComputed { get; set; }
		
		
		public EadInputAssumptionGroupEnum EadGroup { get; set; }
		
		
		[Required]
		public bool RequiresGroupApproval { get; set; }
		
		
		 public Guid? ObeEclId { get; set; }
		 
		 
    }
}