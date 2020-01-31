using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclConfig.Dtos
{
    public class GetAffiliateForEditOutput
    {
		public CreateOrEditAffiliateDto AffiliateConfiguration { get; set; }

    }
}