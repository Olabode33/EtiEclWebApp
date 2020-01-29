using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.Dto;

namespace TestDemo.InvestmentComputation.Dtos
{
    public class CreateOrEditInvestmentEclOverrideDto : CreateOrEditEclOverrideDtoBase
    { 
		public Guid InvestmentEclSicrId { get; set; }
    }
}