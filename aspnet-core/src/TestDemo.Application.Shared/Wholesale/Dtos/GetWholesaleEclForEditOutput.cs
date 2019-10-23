using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.Wholesale.Dtos
{
    public class GetWholesaleEclForEditOutput
    {
		public CreateOrEditWholesaleEclDto WholesaleEcl { get; set; }

		public string UserName { get; set;}


    }
}