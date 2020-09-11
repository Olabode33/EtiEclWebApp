using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.LoanImpairmentInputParameters.Dtos;
using TestDemo.LoanImpairmentHaircuts.Dtos;
using TestDemo.LoanImpairmentRecoveries.Dtos;
using TestDemo.LoanImpairmentScenarios.Dtos;
using TestDemo.LoanImpairmentKeyParameters.Dtos;
using System.Collections.Generic;

namespace TestDemo.LoanImpairmentsRegisters.Dtos
{
    public class CreateOrEditLoanImpairmentRegisterDto : EntityDto<Guid?>
    {

		public CalibrationStatusEnum Status { get; set; }

        public virtual CreateOrEditLoanImpairmentInputParameterDto InputParameter { get; set; }
        public virtual CreateOrEditLoanImpairmentHaircutDto HaircutRecovery { get; set; }
        public virtual List<CreateOrEditLoanImpairmentRecoveryDto> LoanImpairmentRecovery { get; set; }

        public virtual List<CreateOrEditLoanImpairmentScenarioDto> LoanImpairmentScenarios { get; set; }
        public virtual List<CreateOrEditLoanImpairmentKeyParameterDto> CalibrationOfKeyParameters { get; set; }

    }
}