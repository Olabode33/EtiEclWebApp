﻿using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared.Dtos;

namespace TestDemo.Dto.Ecls
{
    public class GetEclForEditOutput
    {
        public CreateOrEditEclDto EclDto { get; set; }
        public string Country { get; set; }
        public string CreatedByUserName { get; set; }
        public string ClosedByUserName { get; set; }
        public List<EadInputAssumptionDto> EadInputAssumptions { get; set; }
        public List<LgdAssumptionDto> LgdInputAssumptions { get; set; }
        public List<PdInputAssumptionDto> PdInputAssumption { get; set; }
        public List<InvSecMacroEconomicAssumptionDto> PdInputAssumptionMacroeconomic { get; set; }
        public List<InvSecFitchCummulativeDefaultRateDto> PdInputFitchCummulativeDefaultRate { get; set; }
        public List<AssumptionDto> FrameworkAssumption { get; set; }
        public List<PdInputAssumptionMacroeconomicInputDto> PdInputAssumptionMacroeconomicInput { get; set; }
        public List<PdInputAssumptionMacroeconomicProjectionDto> PdInputAssumptionMacroeconomicProjections { get; set; }
        public List<PdInputAssumptionNonInternalModelDto> PdInputAssumptionNonInternalModels { get; set; }
        public List<PdInputAssumptionNplIndexDto> PdInputAssumptionNplIndex { get; set; }
        public List<PdInputSnPCummulativeDefaultRateDto> PdInputSnPCummulativeDefaultRate { get; set; }
    }

    public class GetBatchEclForEditOutput
    {
        public CreateOrEditEclDto EclDto { get; set; }
        public string Country { get; set; }
        public string CreatedByUserName { get; set; }
        public string ClosedByUserName { get; set; }
        public List<GetAllEclForWorkspaceDto> SubEcls { get; set; }
    }
}
