using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleInputs.Dtos
{
    public class GetWholesaleEclUploadApprovalForEditOutput
    {
		public CreateOrEditWholesaleEclUploadApprovalDto WholesaleEclUploadApproval { get; set; }

		public string WholesaleEclUploadUploadComment { get; set;}

		public string UserName { get; set;}


    }
}