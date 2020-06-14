using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.CalibrationInput
{
    [Table("MacroenonomicData")]
    public class MacroeconomicData: Entity
    {
        public int? MacroeconomicId { get; set; }
        public double? Value { get; set; }
        public DateTime? Period { get; set; }
        public double? NPL_Percentage_Ratio { get; set; }
        public long? AffiliateId { get; set; }
        public int? MacroId { get; set; }
    }
}
