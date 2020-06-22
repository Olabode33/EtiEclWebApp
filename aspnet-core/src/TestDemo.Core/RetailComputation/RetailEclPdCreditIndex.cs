using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;
using TestDemo.Retail;

namespace TestDemo.RetailComputation
{
    [Table("RetailPDCreditIndex")]
    public class RetailEclPdCreditIndex: EclPdCreditIndexBase
    {
        public virtual Guid RetailEclId { get; set; }
    }
}
