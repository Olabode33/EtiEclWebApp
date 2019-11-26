﻿using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.RetailComputation
{
	[Table("RetailLgdCollateralTypeDatas")]
    public class RetailLgdCollateralTypeData : Entity<Guid> 
    {

		public virtual string CONTRACT_NO { get; set; }
		
		public virtual double DEBENTURE_OMV { get; set; }
		
		public virtual double CASH_OMV { get; set; }
		
		public virtual double INVENTORY_OMV { get; set; }
		
		public virtual string PLANT_AND_EQUIPMENT_OMV { get; set; }
		
		public virtual double RESIDENTIAL_PROPERTY_OMV { get; set; }
		
		public virtual double COMMERCIAL_PROPERTY_OMV { get; set; }
		
		public virtual double RECEIVABLES_OMV { get; set; }
		
		public virtual double SHARES_OMV { get; set; }
		
		public virtual double VEHICLE_OMV { get; set; }
		
		public virtual double DEBENTURE_FSV { get; set; }
		
		public virtual double CASH_FSV { get; set; }
		
		public virtual double INVENTORY_FSV { get; set; }
		
		public virtual double PLANT_AND_EQUIPMENT_FSV { get; set; }
		
		public virtual double RESIDENTIAL_PROPERTY_FSV { get; set; }
		
		public virtual double COMMERCIAL_PROPERTY_FSV { get; set; }
		
		public virtual double RECEIVABLES_FSV { get; set; }
		
		public virtual double SHARES_FSV { get; set; }
		
		public virtual double VEHICLE_FSV { get; set; }
		

		public virtual Guid RetailEclId { get; set; }
		
        [ForeignKey("RetailEclId")]
		public RetailEcl RetailEclFk { get; set; }
		
    }
}