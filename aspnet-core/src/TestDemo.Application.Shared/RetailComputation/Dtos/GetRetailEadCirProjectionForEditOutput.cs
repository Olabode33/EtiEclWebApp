﻿using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetRetailEadCirProjectionForEditOutput
    {
		public CreateOrEditRetailEadCirProjectionDto RetailEadCirProjection { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}