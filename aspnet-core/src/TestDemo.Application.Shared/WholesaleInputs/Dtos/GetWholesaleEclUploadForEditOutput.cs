using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleInputs.Dtos
{
    public class GetWholesaleEclUploadForEditOutput
    {
		public CreateOrEditWholesaleEclUploadDto WholesaleEclUpload { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}