using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.BaseEngine.InputBase
{
    public class EclUploadBase : FullAuditedEntity<Guid>, IMayHaveTenant, IMustHaveOrganizationUnit
    {
        public int? TenantId { get; set; }
        public virtual long OrganizationUnitId { get; set; }

        public virtual UploadDocTypeEnum DocType { get; set; }

        public virtual string UploadComment { get; set; }

        public virtual GeneralStatusEnum Status { get; set; }
        public virtual int AllJobs { get; set; }
        public virtual int CompletedJobs { get; set; }
    }
}
