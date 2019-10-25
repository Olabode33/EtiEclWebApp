using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.ObeInputs.Dtos
{
    public class ObeEclUploadDto : EntityDto<Guid>
    {
		public UploadDocTypeEnum DocType { get; set; }

		public string UploadComment { get; set; }

		public GeneralStatusEnum Status { get; set; }


		 public Guid? ObeEclId { get; set; }

		 
    }
}