using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleInputs.Dtos
{
    public class CreateOrEditWholesaleEclUploadApprovalDto : EntityDto<Guid?>
    {

		public DateTime? ReviewedDate { get; set; }
		
		
		public string ReviewComment { get; set; }
		
		
		public GeneralStatusEnum Status { get; set; }
		
		
		 public Guid WholesaleEclUploadId { get; set; }
		 
		 		 public long? ReviewedByUserId { get; set; }
		 
		 
    }
}