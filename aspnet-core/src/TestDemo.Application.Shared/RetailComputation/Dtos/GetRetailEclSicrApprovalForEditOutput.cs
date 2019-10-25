using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetRetailEclSicrApprovalForEditOutput
    {
		public CreateOrEditRetailEclSicrApprovalDto RetailEclSicrApproval { get; set; }

		public string RetailEclSicrTenantId { get; set;}

		public string UserName { get; set;}


    }
}