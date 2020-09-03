using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using TestDemo.HoldCoAssetBook.Dtos;

namespace TestDemo.IVModels.Dtos
{
    public class CreateOrEditHoldCoRegisterDto : EntityDto<Guid?>
    {
        public virtual CalibrationStatusEnum Status { get; set; }

        public virtual CreateOrEditHoldCoInputParameterDto InputParameter { get; set; }
        public virtual List<CreateOrEditMacroEconomicCreditIndexDto> MacroEconomicCreditIndex { get; set; }
        public virtual List<CreateOrEditAssetBookDto> AssetBook { get; set; }

    }
}