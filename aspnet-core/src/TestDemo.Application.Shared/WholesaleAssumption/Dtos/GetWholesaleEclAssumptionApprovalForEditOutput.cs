using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetWholesaleEclAssumptionApprovalForEditOutput
    {
		public CreateOrEditWholesaleEclAssumptionApprovalDto WholesaleEclAssumptionApproval { get; set; }

		public string WholesaleEclTenantId { get; set;}

		public string UserName { get; set;}


    }
}