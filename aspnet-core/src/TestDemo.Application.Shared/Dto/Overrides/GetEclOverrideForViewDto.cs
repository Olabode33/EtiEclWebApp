using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Dto.Overrides
{
    public class GetEclOverrideForViewDto
    {
        public EclOverrideDto EclOverride { get; set; }
        public string ContractId { get; set; }
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public string ContractNo { get; set; }
        public string AccountNo { get; set; }
        public string CustomerNo { get; set; }
        public string Segment { get; set; }
        public string ProductType { get; set; }
        public string Sector { get; set; }
    }
}
