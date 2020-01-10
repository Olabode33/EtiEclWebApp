using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto
{
    public class CreateOrEditEclPdAssumptionBase: CreateOrEditEclGenericAssumptionBase
    {
        public PdInputAssumptionGroupEnum PdGroup { get; set; }
    }
}
