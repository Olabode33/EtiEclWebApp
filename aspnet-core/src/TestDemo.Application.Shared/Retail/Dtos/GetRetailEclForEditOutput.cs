using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.Retail.Dtos
{
    public class GetRetailEclForEditOutput
    {
		public CreateOrEditRetailEclDto RetailEcl { get; set; }

		public string UserName { get; set;}


    }
}