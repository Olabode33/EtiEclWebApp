﻿
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.ObeComputation.Dtos
{
    public class ObeEadEirProjectionDto : EntityDto<Guid>
    {

		 public Guid ObeEclId { get; set; }

		 
    }
}