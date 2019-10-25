using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetObeEclAssumptionApprovalForEditOutput
    {
		public CreateOrEditObeEclAssumptionApprovalDto ObeEclAssumptionApproval { get; set; }

		public string ObeEclTenantId { get; set;}

		public string UserName { get; set;}


    }
}