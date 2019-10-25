using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailInputs.Dtos
{
    public class GetRetailEclUploadForEditOutput
    {
		public CreateOrEditRetailEclUploadDto RetailEclUpload { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}