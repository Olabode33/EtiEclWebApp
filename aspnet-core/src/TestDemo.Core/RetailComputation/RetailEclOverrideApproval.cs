﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.ComputationBase;
using TestDemo.Retail;

namespace TestDemo.RetailComputation
{
    [Table("RetailEclOverrideApprovals")]
    public class RetailEclOverrideApproval: EclOverrideApprovalBase
    {
        public virtual Guid RetailEclId { get; set; }

        [ForeignKey("RetailEclId")]
        public RetailEcl RetailEclFk { get; set; }
    }
}
