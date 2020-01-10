using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto
{
    public class CreateOrEditEclEadAssumptionBase : CreateOrEditEclGenericAssumptionBase
    {
        public EadInputAssumptionGroupEnum EadGroup { get; set; }
    }
}
