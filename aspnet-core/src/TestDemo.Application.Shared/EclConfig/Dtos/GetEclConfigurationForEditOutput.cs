using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclConfig.Dtos
{
    public class GetEclConfigurationForEditOutput
    {
		public CreateOrEditEclConfigurationDto EclConfiguration { get; set; }


    }
}