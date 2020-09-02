using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.IVModels.Dtos
{
    public class CreateOrEditHoldCoRegisterApprovalDto : EntityDto<Guid>
    {
		public CalibrationStatusEnum Status { get; set; }

        public DateTime ReviewedDate { get; set; }

        public string ReviewComment { get; set; }

        public Guid RegistrationId { get; set; }

        public long ReviewedByUserId { get; set; }

    }
}