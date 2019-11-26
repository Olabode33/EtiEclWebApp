using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetObeLgdContractDataForEditOutput
    {
		public CreateOrEditObeLgdContractDataDto ObeLgdContractData { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}