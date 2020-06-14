using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.CalibrationResult
{
    [Table("MacroResult_PrincipalComponent")]
    public class MacroResult_PrincipalComponent: Entity
    {
        public double? PrincipalComponent1 { get; set; }
        public double? PrincipalComponent2 { get; set; }
        public double? PrincipalComponent3 { get; set; }
        public double? PrincipalComponent4 { get; set; }
        public double? PrincipalComponent5 { get; set; }
        public int MacroId { get; set; }
        public DateTime DateCreated { get; set; }


    }


    [Table("MacroResult_Statistics")]
    public class MacroResult_Statistics : Entity
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


    [Table("MacroResult_CorMat")]
    public class MacroResult_CorMat : Entity
    {
        public double? Value { get; set; }
        public int MacroEconomicIdA { get; set; }
        public int MacroEconomicIdB { get; set; }
        public string MacroEconomicLabelA { get; set; }
        public string MacroEconomicLabelB { get; set; }
        public int MacroId { get; set; }
        public DateTime DateCreated { get; set; }
    }

    [Table("MacroResult_IndexData")]
    public class MacroResult_IndexData : Entity
    {
        public string Period { get; set; }
        public double? Index { get; set; }
        public double? StandardIndex { get; set; }
        public double? BfNpl { get; set; }
        public int MacroId { get; set; }
        public DateTime DateCreated { get; set; }
    }

    [Table("MacroResult_PrincipalComponentSummary")]
    public class MacroResult_PrincipalComponentSummary : Entity
    {
        public double? Value { get; set; }
        public int PrincipalComponentIdA { get; set; }
        public int PrincipalComponentIdB { get; set; }
        public string PricipalComponentLabelA { get; set; }
        public string PricipalComponentLabelB { get; set; }
        public int MacroId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
