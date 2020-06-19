using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.CalibrationResult
{
    [Table("MacroResult_SelectedMacroEconomicVariables")]
    public class MacroResult_SelectedMacroEconomicVariables: Entity
    {
        public int MacroeconomicVariableId { get; set; }
        public long AffiliateId { get; set; }
        public int BackwardOffset { get; set; }
    }
}
