using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.Investment;

namespace TestDemo.InvestmentComputation
{
    [Table("InvestmentEclPdLifetime")]
    public class InvestmentEclPdLifetime : Entity<Guid>
    {
        public virtual string Rating { get; set; }
        public virtual int Month { get; set; }
        public virtual double? BestValue { get; set; }
        public virtual double? OptimisticValue { get; set; }
        public virtual double? DownturnValue { get; set; }
        public virtual Guid EclId { get; set; }
        [ForeignKey("EclId")]
        public InvestmentEcl EclFk { get; set; }
    }
}
