﻿using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;

namespace TestDemo.RetailComputation
{
	[Table("RetailPdLifetimeDownturns")]
    public class RetailPdLifetimeDownturn : EclPdLifetimeBase
	{
		public virtual Guid RetailEclId { get; set; }
    }
}