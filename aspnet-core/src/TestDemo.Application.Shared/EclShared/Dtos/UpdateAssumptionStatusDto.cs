using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Dtos
{
    public class UpdateAssumptionStatusDto: EntityDto<Guid>
    {
        public GeneralStatusEnum Status { get; set; }
    }
}
