using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.EclShared.Dtos
{
    public class EadInputAssumptionDto : EntityDto<Guid>
    {
        public string Key { get; set; }
		public string InputName { get; set; }
		public string Value { get; set; }
        public DataTypeEnum DataType { get; set; }
        public EadInputAssumptionGroupEnum AssumptionGroup { get; set; }
        public bool IsComputed { get; set; }
        public bool RequiresGroupApproval { get; set; }
    }
}