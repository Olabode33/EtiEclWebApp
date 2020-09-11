using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.LoanImpairmentRecoveries.Dtos
{
    public class GetLoanImpairmentRecoveryForEditOutput
    {
		public CreateOrEditLoanImpairmentRecoveryDto LoanImpairmentRecovery { get; set; }


    }
}