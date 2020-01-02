using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.RetailAssumption.Dtos;
using System.Collections.Generic;
using TestDemo.EclShared.Dtos;

namespace TestDemo.Retail.Dtos
{
    public class GetRetailEclForEditOutput
    {
		public CreateOrEditRetailEclDto RetailEcl { get; set; }
        public string Country { get; set; }
        public string CreatedByUserName { get; set; }
		public string ClosedByUserName { get; set;}
        public List<CreateOrEditRetailEclAssumptionDto> FrameworkAssumption { get; set; }
        public List<CreateOrEditRetailEclEadInputAssumptionDto> EadInputAssumptions { get; set; }
        public List<CreateOrEditRetailEclLgdAssumptionDto> LgdInputAssumptions { get; set; }

        public List<PdInputAssumptionDto> PdInputAssumption { get; set; }
        public List<PdInputAssumptionMacroeconomicInputDto> PdInputAssumptionMacroeconomicInput { get; set; }
        public List<PdInputAssumptionMacroeconomicProjectionDto> PdInputAssumptionMacroeconomicProjections { get; set; }
        public List<PdInputAssumptionNonInternalModelDto> PdInputAssumptionNonInternalModels { get; set; }
        public List<PdInputAssumptionNplIndexDto> PdInputAssumptionNplIndex { get; set; }
        public List<PdInputSnPCummulativeDefaultRateDto> PdInputSnPCummulativeDefaultRate { get; set; }
    }
}