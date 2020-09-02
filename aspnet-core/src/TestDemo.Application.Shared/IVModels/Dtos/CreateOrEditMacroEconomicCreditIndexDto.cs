
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.IVModels.Dtos
{
    public class CreateOrEditMacroEconomicCreditIndexDto : EntityDto<Guid?>
    {
		public virtual Guid RegistrationId { get; set; }

		public virtual int Month { get; set; }

		public virtual double BestEstimate { get; set; }

		public virtual double Optimistic { get; set; }

		public virtual double Downturn { get; set; }


	}
}