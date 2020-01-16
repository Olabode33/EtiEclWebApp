using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.Investment;

namespace TestDemo.InvestmentComputation
{
    [Table("InvestmentEclEadInputs")]
    public class InvestmentEclEadInput : Entity<Guid>
    {
        public virtual string AssetDescription { get; set; }
        public virtual string BusinessModelClassification { get; set; }
        public virtual string AssetType { get; set; }
        public virtual string CounterParty { get; set; }
        public virtual double? NominalAmount { get; set; }
        public virtual double? CarryingAmountIFRS { get; set; }
        public virtual DateTime? PurchaseDate { get; set; }
        public virtual DateTime? MaturityDate { get; set; }
        public virtual double? EIR { get; set; }
        public virtual string PrincpalRepayment { get; set; }
        public virtual string RepaymentTerms { get; set; }
        public virtual double? MonthlyInt { get; set; }
        public virtual double? TermInForceMonths { get; set; }
        public virtual double? ContractualTermMonths { get; set; }
        public virtual int? PaymentStartMonth { get; set; }
        public virtual double? PrincipalAmortisation { get; set; }
        public virtual double? Coupon { get; set; }
        public virtual double? Month0 { get; set; }
        public virtual Guid EclId { get; set; }
        [ForeignKey("EclId")]
        public InvestmentEcl EclFk { get; set; }
    }
}
