using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Dtos
{
    public class GetAllEclForWorkspaceInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public long AffiliateId { get; set; }
        public int Portfolio { get; set; }
        public int Status { get; set; }
    }
}
