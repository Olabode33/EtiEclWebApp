using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeInputs.Dtos
{
    public class GetObeEclDataPaymentScheduleForEditOutput
    {
		public CreateOrEditObeEclDataPaymentScheduleDto ObeEclDataPaymentSchedule { get; set; }

		public string ObeEclUploadTenantId { get; set;}


    }
}