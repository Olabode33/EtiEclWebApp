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
}
