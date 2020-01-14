using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentInputs.Dtos
{
    public class CreateOrEditInvestmentEclUploadDto : EntityDto<Guid?>
    {

		public UploadDocTypeEnum DocType { get; set; }
		
		
		public string UploadComment { get; set; }
		
		
		public GeneralStatusEnum Status { get; set; }
		
		
		 public Guid EclId { get; set; }
		 
		 
    }
}