using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.ComputationBase
{
    public class EclLgdCollateralBase: Entity<Guid>
    {
		public virtual string contract_no { get; set; }		
		public virtual string customer_no { get; set; }		
		public virtual double debenture_omv { get; set; }		
		public virtual double cash_omv { get; set; }		
		public virtual double inventory_omv { get; set; }		
		public virtual double plant_and_equipment_omv { get; set; }		
		public virtual double residential_property_omv { get; set; }		
		public virtual double commercial_property_omv { get; set; }		
		public virtual double receivables_omv { get; set; }		
		public virtual double shares_omv { get; set; }		
		public virtual double vehicle_omv { get; set; }		
		public virtual double total_omv { get; set; }
		public virtual double debenture_fsv { get; set; }
		public virtual double cash_fsv { get; set; }
		public virtual double inventory_fsv { get; set; }
		public virtual double plant_and_equipment_fsv { get; set; }
		public virtual double residential_property_fsv { get; set; }
		public virtual double commercial_property_fsv { get; set; }		
		public virtual double receivables_fsv { get; set; }		
		public virtual double shares_fsv { get; set; }		
		public virtual double vehicle_fsv { get; set; }
	}
}
