using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Dtos
{
    public class GetAllPdAssumptionsDto
    {
        public List<PdInputAssumptionDto> PdInputAssumption { get; set; }
        public List<PdInputAssumptionMacroeconomicInputDto> PdInputAssumptionMacroeconomicInput { get; set; }
        public List<PdInputAssumptionMacroeconomicProjectionDto> PdInputAssumptionMacroeconomicProjections { get; set; }
        public List<PdInputAssumptionNonInternalModelDto> PdInputAssumptionNonInternalModels { get; set; }
        public List<PdInputAssumptionNplIndexDto> PdInputAssumptionNplIndex { get; set; }
        public List<PdInputSnPCummulativeDefaultRateDto> PdInputSnPCummulativeDefaultRate { get; set; }
    }
}
