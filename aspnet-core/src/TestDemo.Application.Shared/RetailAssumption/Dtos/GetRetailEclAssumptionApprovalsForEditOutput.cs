using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class GetRetailEclAssumptionApprovalsForEditOutput
    {
		public CreateOrEditRetailEclAssumptionApprovalsDto RetailEclAssumptionApprovals { get; set; }

		public string UserName { get; set;}

		public string RetailEclTenantId { get; set;}


    }
}