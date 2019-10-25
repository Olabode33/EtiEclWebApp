using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailInputs.Dtos
{
    public class CreateOrEditRetailEclUploadApprovalDto : EntityDto<Guid?>
    {

		public DateTime? ReviewedDate { get; set; }
		
		
		public string ReviewComment { get; set; }
		
		
		public GeneralStatusEnum Status { get; set; }
		
		
		 public Guid RetailEclUploadId { get; set; }
		 
		 		 public long? ReviewedByUserId { get; set; }
		 
		 
    }
}