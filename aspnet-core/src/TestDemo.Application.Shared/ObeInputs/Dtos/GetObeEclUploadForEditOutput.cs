using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeInputs.Dtos
{
    public class GetObeEclUploadForEditOutput
    {
		public CreateOrEditObeEclUploadDto ObeEclUpload { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}