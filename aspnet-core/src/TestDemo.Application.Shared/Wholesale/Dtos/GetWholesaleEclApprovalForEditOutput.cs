using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.Wholesale.Dtos
{
    public class GetWholesaleEclApprovalForEditOutput
    {
		public CreateOrEditWholesaleEclApprovalDto WholesaleEclApproval { get; set; }

		public string UserName { get; set;}

		public string WholesaleEclTenantId { get; set;}


    }
}