using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared.Dtos;
using TestDemo.Retail.Dtos;

namespace TestDemo.RetailAssumption.Dtos
{
    public class CreateRetailEclAndAssumptions
    {
        public CreateOrEditRetailEclDto RetailEcl { get; set; }
        public List<AssumptionDto> FrameworkAssumptions { get; set; }
        public List<EadInputAssumptionDto> EadInputAssumptionDtos { get; set; }
        public List<LgdAssumptionDto> LgdInputAssumptionDtos { get; set; }
    }
}
