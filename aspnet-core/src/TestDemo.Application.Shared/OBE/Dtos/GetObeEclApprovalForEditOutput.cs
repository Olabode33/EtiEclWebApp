using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.OBE.Dtos
{
    public class GetObeEclApprovalForEditOutput
    {
		public CreateOrEditObeEclApprovalDto ObeEclApproval { get; set; }

		public string ObeEclTenantId { get; set;}

		public string UserName { get; set;}


    }
}