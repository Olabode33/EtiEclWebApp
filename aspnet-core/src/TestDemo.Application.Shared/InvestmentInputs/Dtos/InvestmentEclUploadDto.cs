using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.InvestmentInputs.Dtos
{
    public class InvestmentEclUploadDto : EntityDto<Guid>
    {
		public UploadDocTypeEnum DocType { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public Guid InvestmentEclId { get; set; }

		 
    }
}