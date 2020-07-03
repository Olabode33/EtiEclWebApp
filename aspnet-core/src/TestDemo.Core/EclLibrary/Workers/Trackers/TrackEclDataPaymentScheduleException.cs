using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.Workers.Trackers
{
    public class TrackEclDataPaymentScheduleException : Entity,  IMustHaveOrganizationUnit
    {
		public virtual Guid EclId { get; set; }
		public virtual long OrganizationUnitId { get; set; }
        public string ContractRefNo { get; set; }
        public DateTime? StartDate { get; set; }
        public string Component { get; set; }
        public int? NoOfSchedules { get; set; }
        public string Frequency { get; set; }
        public double? Amount { get; set; }
        public string Exception { get; set; }
    }
}
