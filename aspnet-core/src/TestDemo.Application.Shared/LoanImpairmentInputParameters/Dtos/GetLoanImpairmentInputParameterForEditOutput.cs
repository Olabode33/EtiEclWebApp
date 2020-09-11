using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.LoanImpairmentInputParameters.Dtos
{
    public class GetLoanImpairmentInputParameterForEditOutput
    {
		public CreateOrEditLoanImpairmentInputParameterDto LoanImpairmentInputParameter { get; set; }


    }
}