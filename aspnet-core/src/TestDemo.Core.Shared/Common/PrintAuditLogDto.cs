using Abp.Application.Services.Dto;
using Abp.Events.Bus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Auditing.Dto
{
    public class PrintAuditLogDto : EntityDto<long>
    {
        public string PropertyName { get; set; }
        public string PropertyTypeFullName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public DateTime? ChangeTime { get; set; }
        public DateTime? CreationTime { get; set; }
        public EntityChangeType? ChangeType { get; set; }
        public string EntityId { get; set; }
        public string EntityTypeFullName { get; set; }
        public string BrowserInfo { get; set; }
        public string ClientIpAddress { get; set; }
        public string ClientName { get; set; }
        public string UserName { get; set; }
        public string ImpersonatorUser { get; set; }
        public long? UserId { get; set; }
        public long? ImpersonatorUserId { get; set; }
    }

    public class GetAuditLogForPrintInput
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? UserId { get; set; }
    }
}
