using TestDemo.RetailInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;
using TestDemo.EclLibrary.BaseEngine.InputBase;

namespace TestDemo.BatchEcls.BatchEclInput
{
	[Table("BatchEclDataLoanBooks")]
    public class BatchEclDataLoanBook : EclDataLoanBookBase
	{
		public virtual Guid BatchId { get; set; }
		
    }
}