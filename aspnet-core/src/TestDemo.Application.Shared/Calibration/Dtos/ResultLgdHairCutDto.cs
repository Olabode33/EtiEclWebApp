using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.Calibration.Dtos
{
    public class ResultLgdHairCutDto : EntityDto
    {
        public double? Debenture { get; set; }
        public double? Cash { get; set; }
        public double? Inventory { get; set; }
        public double? Plant_And_Equipment { get; set; }
        public double? Residential_Property { get; set; }
        public double? Commercial_Property { get; set; }
        public double? Receivables { get; set; }
        public double? Shares { get; set; }
        public double? Vehicle { get; set; }
        public string Comment { get; set; }
        public int? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CalibrationId { get; set; }
    }

    public class GetAllResultLgdHaircutDto
    {
        public ResultLgdHairCutDto Haircut { get; set; }
        public ResultLgdHairCutSummaryDto HaircutSummary { get; set; }
    }
}
