using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetObeEclSicrApprovalForEditOutput
    {
		public CreateOrEditObeEclSicrApprovalDto ObeEclSicrApproval { get; set; }

		public string UserName { get; set;}

		public string ObeEclSicrTenantId { get; set;}


    }
}