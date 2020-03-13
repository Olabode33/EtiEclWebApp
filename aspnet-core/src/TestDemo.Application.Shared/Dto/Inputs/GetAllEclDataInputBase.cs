using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Dto.Inputs
{
    public class GetAllEclDataInputBase : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public string CustomerNoFilter { get; set; }
        public string AccountNoFilter { get; set; }
        public string ContractNoFilter { get; set; }
        public string CustomerNameFilter { get; set; }
        public Guid EclId { get; set; }
    }
}
