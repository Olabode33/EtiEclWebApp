using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.LoanImpairmentApprovals.Dtos
{
    public class GetLoanImpairmentApprovalForEditOutput
    {
		public CreateOrEditLoanImpairmentApprovalDto LoanImpairmentApproval { get; set; }


    }
}