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
    [Table("InvestmentEclDiscountFactor")]
    public class InvestmentEclDiscountFactor : Entity<Guid>
    {
        public virtual Guid RecordId { get; set; }
        public virtual int Month { get; set; }
        public virtual double? Eir { get; set; }
        public virtual double? Value { get; set; }
        public virtual Guid EclId { get; set; }
        [ForeignKey("EclId")]
        public InvestmentEcl EclFk { get; set; }
    }
}
