using TestDemo.ObeInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;
using TestDemo.EclLibrary.BaseEngine.InputBase;

namespace TestDemo.ObeInputs
{
	[Table("ObeEclDataPaymentSchedules")]
    public class ObeEclDataPaymentSchedule : EclDataPaymentScheduleBase
	{
		public virtual Guid? ObeEclUploadId { get; set; }
		
    }
}