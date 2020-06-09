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
    }
}
