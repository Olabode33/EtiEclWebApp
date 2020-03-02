
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
    [Table("InvestmentEclMonthlyResults")]
    public class InvestmentEclMonthlyResult : Entity<Guid>
    {
        public virtual Guid RecordId { get; set; }
        public virtual int Month { get; set; }
        public virtual double? BestValue { get; set; }
        public virtual double? OptimisticValue { get; set; }
        public virtual double? DownturnValue { get; set; }
        public virtual Guid EclId { get; set; }
        [ForeignKey("EclId")]
        public InvestmentEcl EclFk { get; set; }
    }

    [Table("InvestmentEclMonthlyPostOverrideResults")]
    public class InvestmentEclMonthlyPostOverrideResult : Entity<Guid>
    {
        public virtual int? StageOverride { get; set; }
        public virtual Guid RecordId { get; set; }
        public virtual int Month { get; set; }
        public virtual double? BestValue { get; set; }
        public virtual double? OptimisticValue { get; set; }
        public virtual double? DownturnValue { get; set; }
        public virtual Guid EclId { get; set; }
        [ForeignKey("EclId")]
        public InvestmentEcl EclFk { get; set; }
    }
}
