using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class CreateOrEditObeEclLgdAssumptionDto : EntityDto<Guid?>
    {

		public string Key { get; set; }
		
		
		public string InputName { get; set; }
		
		
		public string Value { get; set; }
		
		
		public DataTypeEnum DataType { get; set; }
		
		
		[Required]
		public bool IsComputed { get; set; }
		
		
		public LdgInputAssumptionGroupEnum LgdGroup { get; set; }
		
		
		[Required]
		public bool RequiresGroupApproval { get; set; }
		
		
		 public Guid? ObeEclId { get; set; }
		 
		 
    }
}