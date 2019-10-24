using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetWholesaleEclSicrApprovalForEditOutput
    {
		public CreateOrEditWholesaleEclSicrApprovalDto WholesaleEclSicrApproval { get; set; }

		public string UserName { get; set;}

		public string WholesaleEclSicrOverrideComment { get; set;}


    }
}