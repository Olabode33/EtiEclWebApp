using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailInputs.Dtos
{
    public class CreateOrEditRetailEclUploadDto : EntityDto<Guid?>
    {

		public UploadDocTypeEnum DocType { get; set; }
		
		
		public string UploadComment { get; set; }
		
		
		public GeneralStatusEnum Status { get; set; }
		
		
		 public Guid RetailEclId { get; set; }
		 
		 
    }
}