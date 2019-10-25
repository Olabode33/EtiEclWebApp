using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailInputs.Dtos
{
    public class GetRetailEclUploadApprovalForEditOutput
    {
		public CreateOrEditRetailEclUploadApprovalDto RetailEclUploadApproval { get; set; }

		public string RetailEclUploadTenantId { get; set;}

		public string UserName { get; set;}


    }
}