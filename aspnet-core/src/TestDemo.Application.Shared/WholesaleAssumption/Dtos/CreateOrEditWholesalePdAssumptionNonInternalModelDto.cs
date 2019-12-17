
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class CreateOrEditWholesalePdAssumptionNonInternalModelDto : EntityDto<Guid?>
    {

		public bool CanAffiliateEdit { get; set; }
		
		
		 public Guid WholesaleEclId { get; set; }
		 
		 
    }
}