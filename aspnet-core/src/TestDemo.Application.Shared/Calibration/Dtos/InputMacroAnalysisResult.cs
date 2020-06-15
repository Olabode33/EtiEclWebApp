using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Calibration.Dtos
{
    public class MacroResultPrincipalComponentDto : EntityDto
    {
        public double? PrincipalComponent1 { get; set; }
        public double? PrincipalComponent2 { get; set; }
        public double? PrincipalComponent3 { get; set; }
        public double? PrincipalComponent4 { get; set; }
        public double? PrincipalComponent5 { get; set; }
        public int MacroId { get; set; }
        public DateTime DateCreated { get; set; }


    }

    public class MacroResultStatisticsDto : EntityDto
    {
        public double? IndexWeight1 { get; set; }
        public double? IndexWeight2 { get; set; }
        public double? IndexWeight3 { get; set; }
        public double? IndexWeight4 { get; set; }
        public double? IndexWeight5 { get; set; }
        public double? StandardDev { get; set; }
        public double? Average { get; set; }
        public double? Correlation { get; set; }
        public double? TTC_PD { get; set; }

        public int MacroId { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class MacroResultCorMatDto : EntityDto
    {
        public double? Value { get; set; }
        public int MacroEconomicIdA { get; set; }
        public int MacroEconomicIdB { get; set; }
        public string MacroEconomicLabelA { get; set; }
        public string MacroEconomicLabelB { get; set; }
        public int MacroId { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class MacroResultIndexDataDto : EntityDto
    {
        public string Period { get; set; }
        public double? Index { get; set; }
        public double? StandardIndex { get; set; }
        public double? BfNpl { get; set; }
        public int MacroId { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class MacroResultPrincipalComponentSummaryDto : EntityDto
    {
        public double? Value { get; set; }
        public int PrincipalComponentIdA { get; set; }
        public int PrincipalComponentIdB { get; set; }
        public string PricipalComponentLabelA { get; set; }
        public string PricipalComponentLabelB { get; set; }
        public int MacroId { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class GetAllMacroResultDto
    {
        public List<MacroResultPrincipalComponentDto> PrincipalComponent { get; set; }
        public List<MacroResultStatisticsDto> Statistics { get; set; }
        public List<MacroResultCorMatDto> CorMat { get; set; }
        public List<MacroResultIndexDataDto> IndexData { get; set; }
        public List<MacroResultPrincipalComponentSummaryDto> PrincipalComponentSummary { get; set; }
    }

}
