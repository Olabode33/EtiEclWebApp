﻿using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetObePdLifetimeOptimisticForEditOutput
    {
		public CreateOrEditObePdLifetimeOptimisticDto ObePdLifetimeOptimistic { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}