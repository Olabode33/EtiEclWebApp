using TestDemo.WholesaleInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;
using TestDemo.EclLibrary.BaseEngine.InputBase;

namespace TestDemo.WholesaleInputs
{
	[Table("WholesaleEclDataPaymentSchedules")]
    public class WholesaleEclDataPaymentSchedule : EclDataPaymentScheduleBase
	{
		public virtual Guid WholesaleEclUploadId { get; set; }
		
    }
}