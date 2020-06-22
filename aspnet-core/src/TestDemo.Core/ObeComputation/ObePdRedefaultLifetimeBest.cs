﻿using TestDemo.OBE;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;

namespace TestDemo.ObeComputation
{
	[Table("ObePdRedefaultLifetimeBests")]
    public class ObePdRedefaultLifetimeBest : EclPdRedefaultLifetimeBase
	{

		public virtual Guid ObeEclId { get; set; }
		
    }
}