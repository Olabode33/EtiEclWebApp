﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.Authorization.Users;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.BaseEngine.InputBase
{
    public class EclUploadApprovalBase : FullAuditedEntity<Guid>, IMayHaveTenant, IMustHaveOrganizationUnit
    {
		public int? TenantId { get; set; }
		public virtual long OrganizationUnitId { get; set; }

		public virtual DateTime? ReviewedDate { get; set; }

		public virtual string ReviewComment { get; set; }

		public virtual GeneralStatusEnum Status { get; set; }

		public virtual long? ReviewedByUserId { get; set; }

		[ForeignKey("ReviewedByUserId")]
		public User ReviewedByUserFk { get; set; }

	}
}
