﻿using TestDemo.EclShared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.LoanImpairmentsRegisters
{
	[Table("LoanImpairmentRegisters")]
    public class LoanImpairmentRegister : FullAuditedEntity<Guid> 
    {

		public virtual CalibrationStatusEnum Status { get; set; }
		

    }
}