using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.Calibration.Dtos
{
    public class ResultNewPdCommsConsDto : EntityDto
    {
        public int Month { get; set; }
        public double? Comm_1 { get; set; }
        public double? Cons_1 { get; set; }
        public double? Comm_2 { get; set; }
        public double? Cons_2 { get; set; }
        //public string Comment { get; set; }
        //public int? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CalibrationId { get; set; }
    }

    public class GetAllResultPdCommsConsDto
    {
        public List<ResultNewPdCommsConsDto> PdCommsCons { get; set; }
    }
}
