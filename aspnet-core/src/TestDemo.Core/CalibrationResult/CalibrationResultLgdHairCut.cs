using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.CalibrationResult
{
    [Table("CalibrationResult_LGD_HairCut")]
    public class CalibrationResultLgdHairCut: Entity
    {
        public virtual double? Debenture { get; set; }
        public virtual double? Cash { get; set; }
        public virtual double? Inventory { get; set; }
        public virtual double? Plant_And_Equipment { get; set; }
        public virtual double? Residential_Property { get; set; }
        public virtual double? Commercial_Property { get; set; }
        public virtual double? Receivables { get; set; }
        public virtual double? Shares { get; set; }
        public virtual double? Vehicle { get; set; }
        public virtual string Comment { get; set; }
        public virtual int? Status { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual Guid? CalibrationId { get; set; }
    }
}
