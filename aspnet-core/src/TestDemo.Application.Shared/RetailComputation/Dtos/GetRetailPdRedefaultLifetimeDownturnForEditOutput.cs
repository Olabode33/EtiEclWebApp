﻿using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetRetailPdRedefaultLifetimeDownturnForEditOutput
    {
		public CreateOrEditRetailPdRedefaultLifetimeDownturnDto RetailPdRedefaultLifetimeDownturn { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}