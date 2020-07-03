using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.CalibrationInput
{
    [Table("CalibrationHistory_LGD_HairCut")]
    public class CalibrationHistoryLgdHairCut : Entity
    {
        public virtual string Customer_No { get; set; }
        public virtual string Account_No { get; set; }
        public virtual string Contract_No { get; set; }
        public virtual DateTime? Snapshot_Date { get; set; }
        public virtual int? Period { get; set; }
        public virtual double? Outstanding_Balance_Lcy { get; set; }
        public virtual double? Debenture_OMV { get; set; }
        public virtual double? Debenture_FSV { get; set; }
        public virtual double? Cash_OMV { get; set; }
        public virtual double? Cash_FSV { get; set; }
        public virtual double? Inventory_OMV { get; set; }
        public virtual double? Inventory_FSV { get; set; }
        public virtual double? Plant_And_Equipment_OMV { get; set; }
        public virtual double? Plant_And_Equipment_FSV { get; set; }
        public virtual double? Residential_Property_OMV { get; set; }
        public virtual double? Residential_Property_FSV { get; set; }
        public virtual double? Commercial_Property_OMV { get; set; }
        public virtual double? Commercial_Property_FSV { get; set; }
        public virtual double? Receivables_OMV { get; set; }
        public virtual double? Receivables_FSV { get; set; }
        public virtual double? Shares_OMV { get; set; }
        public virtual double? Shares_FSV { get; set; }
        public virtual double? Vehicle_OMV { get; set; }
        public virtual double? Vehicle_FSV { get; set; }
        public virtual double? Guarantee_Value { get; set; }
        public virtual FrameworkEnum? ModelType { get; set; }
        public virtual long? AffiliateId { get; set; }
        public virtual DateTime? DateCreated { get; set; }
    }
}
