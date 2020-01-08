using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.EclShared.Dtos
{
    public class CreateOrEditInvSecMacroEconomicAssumptionDto : EntityDto<Guid?>
    {

		public int Month { get; set; }
		
		
		public double BestValue { get; set; }
		
		
		public double OptimisticValue { get; set; }
		
		
		public double DownturnValue { get; set; }
		
		
		public GeneralStatusEnum Status { get; set; }
		
		
		public bool CanAffiliateEdit { get; set; }
		
		
		public bool RequiresGroupApproval { get; set; }
		
		
		public long OrganizationUnitId { get; set; }
		
		

    }
}