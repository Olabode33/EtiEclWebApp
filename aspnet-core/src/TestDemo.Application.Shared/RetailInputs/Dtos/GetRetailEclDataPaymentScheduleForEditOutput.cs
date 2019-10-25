using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailInputs.Dtos
{
    public class GetRetailEclDataPaymentScheduleForEditOutput
    {
		public CreateOrEditRetailEclDataPaymentScheduleDto RetailEclDataPaymentSchedule { get; set; }

		public string RetailEclUploadTenantId { get; set;}


    }
}