using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.HoldCoApprovals.Dtos
{
    public class GetHoldCoApprovalForEditOutput
    {
		public CreateOrEditHoldCoApprovalDto HoldCoApproval { get; set; }


    }
}