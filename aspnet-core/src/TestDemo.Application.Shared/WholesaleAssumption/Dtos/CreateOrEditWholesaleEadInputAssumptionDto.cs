using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class CreateOrEditWholesaleEadInputAssumptionDto : EntityDto<Guid?>
    {

		public string Key { get; set; }
		
		
		public string InputName { get; set; }
		
		
		public string Value { get; set; }
		
		
		public DataTypeEnum Datatype { get; set; }
		
		
		public bool IsComputed { get; set; }
		
		
		public EadInputGroupEnum EadGroup { get; set; }
		
		
		public bool RequiresGroupApproval { get; set; }
		
		
		 public Guid WholesaleEclId { get; set; }
		 
		 
    }
}