using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.InputBase
{
    public class EclDataPaymentScheduleBase : FullAuditedEntity<Guid>, IMayHaveTenant, IMustHaveOrganizationUnit
	{
		public int? TenantId { get; set; }
		public virtual long OrganizationUnitId { get; set; }

		public virtual string ContractRefNo { get; set; }

		public virtual DateTime? StartDate { get; set; }

		public virtual string Component { get; set; }

		public virtual int? NoOfSchedules { get; set; }

		public virtual string Frequency { get; set; }

		public virtual double? Amount { get; set; }


	}
}
