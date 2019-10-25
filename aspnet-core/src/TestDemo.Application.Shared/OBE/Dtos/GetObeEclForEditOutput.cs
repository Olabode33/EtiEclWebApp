using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.OBE.Dtos
{
    public class GetObeEclForEditOutput
    {
		public CreateOrEditObeEclDto ObeEcl { get; set; }

		public string UserName { get; set;}


    }
}