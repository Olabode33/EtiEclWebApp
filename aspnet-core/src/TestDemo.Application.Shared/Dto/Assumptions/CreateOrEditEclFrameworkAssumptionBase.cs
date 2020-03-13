using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto
{
    public class CreateOrEditEclFrameworkAssumptionBase: CreateOrEditEclGenericAssumptionBase
    {
        public AssumptionGroupEnum AssumptionGroup { get; set; }
    }
}
