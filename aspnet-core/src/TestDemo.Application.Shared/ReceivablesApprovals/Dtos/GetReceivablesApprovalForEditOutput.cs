using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ReceivablesApprovals.Dtos
{
    public class GetReceivablesApprovalForEditOutput
    {
		public CreateOrEditReceivablesApprovalDto ReceivablesApproval { get; set; }


    }
}