using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.Retail.Dtos
{
    public class GetRetailEclApprovalForEditOutput
    {
		public CreateOrEditRetailEclApprovalDto RetailEclApproval { get; set; }

		public string UserName { get; set;}

		public string RetailEclTenantId { get; set;}


    }
}