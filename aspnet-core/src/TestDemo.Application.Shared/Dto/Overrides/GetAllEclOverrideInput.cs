using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Dto.Overrides
{
    public class GetAllEclOverrideInput : PagedAndSortedResultRequestDto
    {
        public Guid EclId { get; set; }

        public string Filter { get; set; }

        public int StatusFilter { get; set; }

        public string ContractIdFilter { get; set; }
    }
}
