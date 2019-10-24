using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleInputs.Dtos
{
    public class GetWholesaleEclDataPaymentScheduleForEditOutput
    {
		public CreateOrEditWholesaleEclDataPaymentScheduleDto WholesaleEclDataPaymentSchedule { get; set; }

		public string WholesaleEclUploadUploadComment { get; set;}


    }
}