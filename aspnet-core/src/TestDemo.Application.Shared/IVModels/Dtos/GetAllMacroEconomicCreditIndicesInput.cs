using Abp.Application.Services.Dto;
using System;

namespace TestDemo.IVModels.Dtos
{
    public class GetAllMacroEconomicCreditIndicesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}