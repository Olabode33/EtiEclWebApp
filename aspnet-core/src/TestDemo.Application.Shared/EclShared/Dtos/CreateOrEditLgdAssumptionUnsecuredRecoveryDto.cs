using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclShared.Dtos
{
    public class CreateOrEditLgdAssumptionUnsecuredRecoveryDto : EntityDto<Guid?>
    {

		public string InputName { get; set; }		
		
		public string Value { get; set; }
        
    }
}