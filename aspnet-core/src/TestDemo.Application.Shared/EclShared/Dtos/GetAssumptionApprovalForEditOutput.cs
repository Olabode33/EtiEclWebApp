﻿using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclShared.Dtos
{
    public class GetAssumptionApprovalForEditOutput
    {
		public CreateOrEditAssumptionApprovalDto AssumptionApproval { get; set; }

		public string UserName { get; set;}


    }
}