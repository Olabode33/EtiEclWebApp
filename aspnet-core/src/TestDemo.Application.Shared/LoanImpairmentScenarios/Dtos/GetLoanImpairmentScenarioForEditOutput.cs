using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.LoanImpairmentScenarios.Dtos
{
    public class GetLoanImpairmentScenarioForEditOutput
    {
		public CreateOrEditLoanImpairmentScenarioDto LoanImpairmentScenario { get; set; }


    }
}