using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeInputs.Dtos
{
    public class GetObeEclUploadApprovalForEditOutput
    {
		public CreateOrEditObeEclUploadApprovalDto ObeEclUploadApproval { get; set; }

		public string ObeEclUploadTenantId { get; set;}

		public string UserName { get; set;}


    }
}