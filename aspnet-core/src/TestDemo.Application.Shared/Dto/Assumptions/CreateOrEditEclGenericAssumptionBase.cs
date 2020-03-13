using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto
{
    public class CreateOrEditEclGenericAssumptionBase : EntityDto<Guid?>
    {
        public string Key { get; set; }
        public string InputName { get; set; }
        public string Value { get; set; }
        public DataTypeEnum DataType { get; set; }
        public bool IsComputed { get; set; }
        public bool CanAffiliateEdit { get; set; }
        public bool RequiresGroupApproval { get; set; }
    }
}
