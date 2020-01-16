using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.Investment;

namespace TestDemo.InvestmentComputation
{
    /// <summary>
    /// Populated by stored procedure [InvSec_proc_ComputeLifetimeEadLifetime]
    /// </summary>
    [Table("InvestmentEclSicr")]
    public class InvestmentEclSicr : Entity<Guid>
    {
        public virtual Guid RecordId { get; set; }
        public virtual string AssetDescription { get; set; }
        public virtual string AssetType { get; set; }
        public virtual string SovereignDebt { get; set; }
        public virtual string CurrentCreditRating { get; set; }
        public virtual string PrudentialClassification { get; set; }
        public virtual string ForebearanceFlag { get; set; }
        public virtual string FitchRating { get; set; }
        public virtual int StageClassification { get; set; }
        public virtual int StageForebearance { get; set; }
        public virtual int StageRating { get; set; }
        public virtual int FinalStage { get; set; }
        public virtual Guid EclId { get; set; }
        [ForeignKey("EclId")]
        public InvestmentEcl EclFk { get; set; }
    }
}
