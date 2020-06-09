using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetAllObeEclOverridesInput : PagedAndSortedResultRequestDto
    {
        public Guid EclId { get; set; }
        public string Filter { get; set; }
        public int StatusFilter { get; set; }
        public string ObeEclDataLoanBookCustomerNameFilter { get; set; }


    }
}