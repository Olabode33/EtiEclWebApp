using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.LoanImpairmentsRegisters.Dtos
{
    public class GetLoanImpairmentRegisterForEditOutput
    {
		public CreateOrEditLoanImpairmentRegisterDto LoanImpairmentRegister { get; set; }


    }
}