using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.InvestmentComputation.Dtos;

namespace TestDemo.Dto.Overrides
{
    public class GetPreResultForOverrideOutput
    {
        public CreateOrEditEclOverrideDto EclOverrides { get; set; }
        public string ContractId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public int Stage { get; set; }
        public double? Impairment { get; set; }
    }

    public class GetPreResultForOverrideNewOutput
    {
        public CreateOrEditEclOverrideNewDto EclOverrides { get; set; }
        public string ContractNo { get; set; }
        public string AccountNo { get; set; }
        public string CustomerNo { get; set; }
        public string Segment { get; set; }
        public string ProductType { get; set; }
        public string Sector { get; set; }
        public int? Stage { get; set; }
        public double? Outstanding_Balance { get; set; }
        public double? Impairment { get; set; }
    }
}
