using TestDemo.EclShared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;

namespace TestDemo.EclShared
{
	[Table("InvSecFitchCummulativeDefaultRates")]
    [Audited]
    public class InvSecFitchCummulativeDefaultRate : FullAuditedEntity<Guid>, IMustHaveOrganizationUnit
    {

		public virtual string Key { get; set; }
		
		public virtual string Rating { get; set; }
		
		public virtual int Year { get; set; }
		
		public virtual double Value { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		
		public virtual long OrganizationUnitId { get; set; }
		

    }
}