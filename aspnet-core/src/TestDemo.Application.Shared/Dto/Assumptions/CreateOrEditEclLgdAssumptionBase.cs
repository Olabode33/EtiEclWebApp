using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.Dto
{
    public class CreateOrEditEclLgdAssumptionBase: CreateOrEditEclGenericAssumptionBase
    {
        public LdgInputAssumptionGroupEnum LgdGroup { get; set; }
    }
}
