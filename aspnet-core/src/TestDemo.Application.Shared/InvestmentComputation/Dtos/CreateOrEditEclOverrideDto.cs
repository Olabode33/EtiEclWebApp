using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.Dto;

namespace TestDemo.InvestmentComputation.Dtos
{
    public class CreateOrEditEclOverrideDto : CreateOrEditEclOverrideDtoBase
    { 
		public Guid EclSicrId { get; set; }
        public string ContractId { get; set; }
    }
}