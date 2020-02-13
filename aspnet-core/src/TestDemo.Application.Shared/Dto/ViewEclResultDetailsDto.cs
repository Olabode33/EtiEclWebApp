using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Dto
{
    public class ViewEclResultDetailsDto
    {
        public string ContractId { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public string Segment { get; set; }
        public string ProductType { get; set; }
        public string Sector { get; set; }
        public int Staging { get; set; }
        public double? Exposure { get; set; }
        public EclResultOverrideFigures PreOverrideResult { get; set; }
        public EclResultOverrideFigures PostOverrideResult { get; set; }
    }

    public class EclResultOverrideFigures
    {
        public double? EclBest { get; set; }
        public double? EclOptimistic { get; set; }
        public double? EclDownturn { get; set; }
        public double? Impairment { get; set; }
    }
}
