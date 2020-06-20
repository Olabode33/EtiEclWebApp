using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.FinalReportBase
{
    public class FrameworkReportDetailBase: Entity<Guid>
    {
        public string ContractNo { get; set; }
        public string AccountNo { get; set; }
        public string CustomerNo { get; set; }
        public string Segment { get; set; }
        public string ProductType { get; set; }
        public string Sector { get; set; }
        public int? Stage { get; set; }
        public double? Outstanding_Balance { get; set; }
        public double? ECL_Best_Estimate { get; set; }
        public double? ECL_Optimistic { get; set; }
        public double? ECL_Downturn { get; set; }
        public double? Impairment_ModelOutput { get; set; }
        public int? Overrides_Stage { get; set; }
        public double? Overrides_TTR_Years { get; set; }
        public double? Overrides_FSV { get; set; }
        public double? Overrides_Overlay { get; set; }
        public double? Overrides_ECL_Best_Estimate { get; set; }
        public double? Overrides_ECL_Optimistic { get; set; }
        public double? Overrides_ECL_Downturn { get; set; }
        public double? Overrides_Impairment_Manual { get; set; }
    }
}
